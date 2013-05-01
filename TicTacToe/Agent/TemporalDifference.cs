using System;
using System.Xml;
using TicTacToe.Game;
using System.Collections.Generic;

namespace TicTacToe.Agent
{
	public class TemporalDifference : TicTacToeAgent
	{
		private string previousState = "";

        /// <summary>
        /// Temporal Difference Constructor
        /// </summary>
        /// <param name="player"></param>
		public TemporalDifference(Player player)
		{
			this.player = player;

            //Read in the configuration
            ReadConfigFile("TDConfig.xml", "TemporalDifference");

            //Read in states
            ReadStates("TDstates.xml", "TemporalDifference");
		}

        /// <summary>
        /// Find the best move and return it's answer.
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="currentState"></param>
		public void innerUpdate(double newValue, string currentState)
		{
			bool found = false;

            List<string> mirrorStates = getAlternativeStates(currentState);

            //loop through all seen states from file
			for(int j = 0; j< MAX; j++)
			{
                //if a state already exists in file
                if (mirrorStates.Exists(n => n == states[0,j]))
				{
					found = true;
					this.states[1,j] = Convert.ToString(double.Parse(this.states[1,j]) + this.StepSize*(newValue - double.Parse(this.states[1,j])));
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
			XmlTextWriter TDstates = new XmlTextWriter("TDstates.xml",System.Text.Encoding.Default);
			TDstates.WriteStartDocument(true);
			TDstates.WriteStartElement("TemporalDifference");
			int inc = 0;
			while(inc< MAX && states[0,inc] != null)
			{
				TDstates.WriteStartElement("state");
				TDstates.WriteAttributeString("string",states[0,inc]);
				TDstates.WriteString(states[1,inc]);
				TDstates.WriteEndElement();
				inc++;
			}
			TDstates.WriteEndElement();
			TDstates.WriteEndDocument();
			TDstates.Close();
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

			//initialize the values to a low number
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
			if(this.previousState != "")
			{
				innerUpdate(newValue, this.previousState);
                this.previousState = "";
			}

            moves++;
            for (int i = 0; i < BOARD_SIZE; i++)
			{
				this.previousState = this.previousState.Insert(this.previousState.Length,board[i].ToString());
			}

			return move;
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

            UpdateStates("TDstates.xml", "TemporalDifference");

			bool found;

            List<string> mirrorStates = getAlternativeStates(currentState);

			//update values
			found = false;
			for(int j = 0; j < MAX; j++)
			{
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

			//write to xml
			XmlTextWriter TDstates = new XmlTextWriter("TDstates.xml",System.Text.Encoding.Default);
			TDstates.WriteStartDocument(true);
			TDstates.WriteStartElement("TemporalDifference");
			int inc = 0;
			while(inc< MAX && states[0,inc] != null)
			{
				TDstates.WriteStartElement("state");
				TDstates.WriteAttributeString("string",states[0,inc]);
				TDstates.WriteString(states[1,inc]);
				TDstates.WriteEndElement();
				inc++;
			}
			TDstates.WriteEndElement();
			TDstates.WriteEndDocument();
			TDstates.Close();
		}

	}
}
