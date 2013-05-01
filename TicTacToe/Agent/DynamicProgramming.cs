using System;
using System.Xml;
using TicTacToe.Game;
using System.Collections.Generic;

namespace TicTacToe.Agent
{
	public class DynamicProgramming: TicTacToeAgent
	{	
		private string previousState = "";

        /// <summary>
        /// Dynamic Programming Constructor
        /// </summary>
        /// <param name="player"></param>
		public DynamicProgramming(Player player)
		{
			this.player = player;

            //Read in the configuration
            ReadConfigFile("DPConfig.xml", "DynamicProgramming");

            //Read in states
            ReadStates("DPstates.xml", "DynamicProgramming");
		}

        /// <summary>
        /// Updates the xml file during the game
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="currentState"></param>
		public void innerUpdate(double newValue, string currentState)
		{
			bool found = false;
			for(int j = 0; j< MAX; j++)
			{
                List<string> mirrorStates = getAlternativeStates(currentState);

				if(mirrorStates.Exists(n => n == this.states[0,j]))
				{
					found = true;
					this.states[1,j] = newValue.ToString();
                    break;
				}
			}
			//A new state is found
			if(found == false)
			{
				int location = 0;
				//traverse through
				while(states[0,location] != null)
				{
					location++;
				}
				this.states[0,location] = currentState;
				this.states[1,location] = newValue.ToString();
			}

			//write to xml
			XmlTextWriter DPstates = new XmlTextWriter("DPstates.xml",System.Text.Encoding.Default);
			DPstates.WriteStartDocument(true);
			DPstates.WriteStartElement("DynamicProgramming");
			int inc = 0;
			while(inc< MAX && states[0,inc] != null)
			{
				DPstates.WriteStartElement("state");
				DPstates.WriteAttributeString("string",states[0,inc]);
				DPstates.WriteString(states[1,inc]);
				DPstates.WriteEndElement();
				inc++;
			}
			DPstates.WriteEndElement();
			DPstates.WriteEndDocument();
			DPstates.Close();
		}

        /// <summary>
        /// Find the best move and return it's answer.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
		public int getMove(int[] board)
		{
			string currentState = "";
			double newValue = 0.0;
            double[] values = new double[BOARD_SIZE];

			//initialize the values to an impossible low number
			for(int i = 0; i < BOARD_SIZE; i++)
			{
                values[i] = double.MinValue;
				currentState = currentState.Insert(currentState.Length,board[i].ToString());
			}

            SetDefaultValue();

            values = FindMovesWinPercentage(board, values);

            if (AutoExplore)
            {
                CheckEpsilonRange(board, values);
            }

			
			board[move] = (int)this.player;
			newValue = findDPValue(board);
			this.previousState = "";
            
            moves++;
            for (int i = 0; i < BOARD_SIZE; i++)
			{
				this.previousState = this.previousState.Insert(this.previousState.Length,board[i].ToString());
			}
			innerUpdate(newValue, this.previousState);
			return move;
		}

        /// <summary>
        /// Find Dynamic Programming Move Value
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
		private double findDPValue(int[] board)
		{
			string state;
			int[] opponentBoard;
			double newValue = 0;
			int count = 0;

            for (int i = 0; i < BOARD_SIZE; i++)
			{
				if(board[i] == 0)
				{
					//Get possible opponents moves
                    opponentBoard = new int[BOARD_SIZE];
					for(int j = 0; j< BOARD_SIZE; j++)
					{
						if(i != j)
						{
							opponentBoard[j] = board[j];
						}
						else
						{
							if(this.player == Player.First)
							{
								opponentBoard[j] = (int)Player.Second;
							}
							else //this.player == second player
							{
								opponentBoard[j] = (int)Player.First;
							}
						}
					}
					//Now find current players possible moves 
                    for (int k = 0; k < BOARD_SIZE; k++)
					{
						if(opponentBoard[k] == 0)
						{
							state = "";
                            for (int m = 0; m < BOARD_SIZE; m++)
							{
								//convert it to string
								if(k != m)
								{
									state = state.Insert(state.Length,opponentBoard[m].ToString());
								}
								else
								{
									if(this.player == Player.First)
									{
										state = state.Insert(state.Length,((int)this.player).ToString());
									}
									else //this.player == second player
									{
                                        state = state.Insert(state.Length, ((int)this.player).ToString());
									}
								}
							}

                            List<string> mirrorStates = getAlternativeStates(state);
							
							//find that state, add it up and increment count 
							bool found = false;
							for(int l = 0; l< MAX; l++)
							{
								if(mirrorStates.Exists(n => n == this.states[0,l]))
								{
									found = true;
									newValue = newValue + double.Parse(this.states[1,l]);
									count++;
                                    break;
								}
							}
							if(found == false)
							{
								newValue = newValue + this.defaultValue;
								count++;
							}
						}
					}
				}
			}
			newValue = newValue/count;
			if(double.IsNaN(newValue))
			{
				newValue = this.defaultValue;
			}
			else if(double.IsPositiveInfinity(newValue) && count != 0)
			{
				newValue = WinReward;
			}
			else if(double.IsNegativeInfinity(newValue) && count != 0)
			{
				newValue = LostReward;
			}
			else if(count == 0)
			{
				newValue = TieReward;
			}

			return (newValue);
		}

        /// <summary>
        /// update the xml file after the game
        /// </summary>
        /// <param name="result"></param>
        /// <param name="board"></param>
        /// <param name="gameType"></param>
        public void update(GameResult result, int[] board, GameType gameType)
		{
			string currentState = "";
            for (int i = 0; i < BOARD_SIZE; i++)
			{
				currentState = currentState.Insert(currentState.Length,board[i].ToString());
			}

            UpdateStates("DPstates.xml", "DynamicProgramming");

            List<string> mirrorStates = getAlternativeStates(currentState);

			bool found;
			//update values
			found = false;
			for(int j = 0; j< MAX; j++)
			{
				/*
				 * Updated for optimization for version 1.1 on 3/3/06
				 */
				if(mirrorStates.Exists(n => n == this.states[0,j]))
				{
					found = true;
                    if (result == GameResult.Win)
					{
						this.states[1,j] = this.WinReward.ToString();
					}
                    else if (result == GameResult.Tie)
					{
						this.states[1,j] = this.TieReward.ToString();
					}
					else //result == -1
					{
						this.states[1,j] = this.LostReward.ToString();
					}
                    break;
				}
			}
			//A new state is found
			if(found == false)
			{
				int location = 0;
				while(states[0,location] != null)
				{
					location++;
				}
				this.states[0,location] = currentState;
                if (result == GameResult.Win)
				{
					this.states[1,location] = this.WinReward.ToString();
				}
                else if (result == GameResult.Tie)
				{
					this.states[1,location] = this.TieReward.ToString();
				}
				else //result == -1
				{
					this.states[1,location] = this.LostReward.ToString();
				}
			}

			//System.Windows.Forms.MessageBox.Show("State: "+currentState+"\nReward: "+result);


			//write to xml
			XmlTextWriter DPstates = new XmlTextWriter("DPstates.xml",System.Text.Encoding.Default);
			DPstates.WriteStartDocument(true);
			DPstates.WriteStartElement("DynamicProgramming");
			int inc = 0;
			while(inc< MAX && states[0,inc] != null)
			{
				DPstates.WriteStartElement("state");
				DPstates.WriteAttributeString("string",states[0,inc]);
				DPstates.WriteString(states[1,inc]);
				DPstates.WriteEndElement();
				inc++;
			}
			DPstates.WriteEndElement();
			DPstates.WriteEndDocument();
			DPstates.Close();
		}
	}
}
