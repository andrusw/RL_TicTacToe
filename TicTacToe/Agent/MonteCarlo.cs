using System;
using System.Xml;
using TicTacToe.Game;

namespace TicTacToe.Agent
{
	public class MonteCarlo
    {
        #region Configuration
        public double Epsilon { get; set; }
        public double StepSize { get; set; }
        public int WinReward { get; set; }
        public int TieReward { get; set; }
        public int LostReward { get; set; }
        public bool AutoExplore { get; set; }
        #endregion

        #region Constants
        private const int MAX_STATES = 5;
        private const int BOARD_SIZE = 9;
        #endregion

        private int outOfBoundsReward;
		private double largestValue = Double.MinValue;
		private int move;//represents the decision of where to move
		private int moves = 0;//represents the number of moves made;
		//Keep track for deep backup
		private string[] visitedStates = new string[MAX_STATES];
		/*
		 * Changed from 7000 to 1000 
		 * for the optimized version 1.1 on 3/6/06
		 */
		//Read/Write states in the array/file
		private const int MAX = 800; 
		//[state,value] x MAX
		private string [,] states = new string[2,MAX];
		private Player player;
		private double defaultValue = 2.0;

        /// <summary>
        /// Monte Carlo Constructor
        /// </summary>
        /// <param name="player"></param>
		public MonteCarlo(Player player)
		{
			this.player = player;

            /*Set configuration default values*/
            Epsilon = 0.1;
		    StepSize = 0.1;
		    WinReward = 1;
		    TieReward = 0;
		    LostReward = -1;
		    AutoExplore = true;

			/*
			 * Read in the configuration
			 */ 
			try
			{
				XmlTextReader MCconfig = new XmlTextReader("MCConfig.xml");
				
				MCconfig.ReadStartElement("MonteCarlo");
				MCconfig.ReadStartElement("Epsilon");
				Epsilon = double.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Stepsize");
				StepSize = double.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Reward");
				MCconfig.ReadStartElement("Win");
				WinReward = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Tie");
				TieReward = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Loss");
				LostReward = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("AutoExplore");
				string ans = MCconfig.ReadString();
				if(ans == "Yes")
				{
					AutoExplore = true;
				}
				else //ans == no
				{
					AutoExplore = false;
				}
				MCconfig.Close();

			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}

			outOfBoundsReward = WinReward + 1;

			/*
			 * Read in the seen states
			 * 
			 * <?xml>
			 * <MonteCarlo>
			 * <state string="?????????">value</state>
			 * </MonteCarlo>
			 * </xml>
			 */ 
			try
			{
				XmlTextReader MCstates = new XmlTextReader("MCstates.xml");
				MCstates.ReadStartElement("MonteCarlo");
				int i = 0;
				this.states[0,i] = MCstates.GetAttribute("string");
				this.states[1,i] = MCstates.ReadString();
				i++;
				while(MCstates.Read())
				{
					this.states[0,i] = MCstates.GetAttribute("string");
					this.states[1,i] = MCstates.ReadString();
					i++;
				}
				MCstates.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}
		}

        /// <summary>
        /// Updates states seen throughout the game.
        /// MonteCarlo updates at the end of each game
        /// </summary>
        /// <param name="result"></param>
        /// <param name="board"></param>
        /// <param name="gameType"></param>
        public void update(GameResult result, int[] board, GameType gameType)
		{
            //If both agents are MonteCarlo
			if(gameType == GameType.AgentVsAgent)
			{
				//Read in values, incase the other agent has updated it;
				try
				{
					XmlTextReader MCstatesRead = new XmlTextReader("MCstates.xml");
					MCstatesRead.ReadStartElement("MonteCarlo");
					int i = 0;
					this.states[0,i] = MCstatesRead.GetAttribute("string");
					this.states[1,i] = MCstatesRead.ReadString();
					i++;
					while(MCstatesRead.Read())
					{
						this.states[0,i] = MCstatesRead.GetAttribute("string");
						this.states[1,i] = MCstatesRead.ReadString();
						i++;
					}
					MCstatesRead.Close();
				}
				catch(System.IO.FileNotFoundException)
				{
					//do nothing, use defaults
				}
				catch(Exception err)
				{
					System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				}
			}

			bool found;
			//update values
			//for each of the moves
			for(int i = 0; i< MAX_STATES; i++)
			{
				found = false;
				//search the list to see if the state is there
				for(int j = 0; j< MAX; j++)
				{

					/*
					 * Changed for version 1.1 on 3/3/06
					 * 
					 */
					int rotationalFound = -1;


					string visitedState1 = "";
					string visitedState2 = "";
					string visitedState3 = "";
					string visitedState4 = "";
					string visitedState5 = "";
					string visitedState6 = "";
					string visitedState7 = "";

					if(this.visitedStates[i] != null)
					{
						visitedState1 = (this.visitedStates[i][2].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][8].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][0].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][6].ToString());
						visitedState2 = (this.visitedStates[i][8].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][6].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][2].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][0].ToString());
						visitedState3 = (this.visitedStates[i][6].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][0].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][8].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][2].ToString());
						
						visitedState4 = (this.visitedStates[i][6].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][8].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][0].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][2].ToString());
						visitedState5 = (this.visitedStates[i][2].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][0].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][8].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][6].ToString());
						visitedState6 = (this.visitedStates[i][8].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][2].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][6].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][0].ToString());
						visitedState7 = (this.visitedStates[i][0].ToString() + this.visitedStates[i][3].ToString() + this.visitedStates[i][6].ToString() + this.visitedStates[i][1].ToString() + this.visitedStates[i][4].ToString() + this.visitedStates[i][7].ToString() + this.visitedStates[i][2].ToString() + this.visitedStates[i][5].ToString() + this.visitedStates[i][8].ToString());				
					
					}


					if(this.visitedStates[i] == this.states[0,j])
					{
						rotationalFound = 0;
					}
					else if(visitedState1 == this.states[0,j])
					{
						rotationalFound = 1;
					}
					else if(visitedState2 == this.states[0,j])
					{
						rotationalFound = 2;
					}
					else if(visitedState3 == this.states[0,j])
					{
						rotationalFound = 3;
					}
					else if(visitedState4 == this.states[0,j])
					{
						rotationalFound = 4;
					}
					else if(visitedState5 == this.states[0,j])
					{
						rotationalFound = 5;
					}
					else if(visitedState6 == this.states[0,j])
					{
						rotationalFound = 6;
					}
					else if(visitedState7 == this.states[0,j])
					{
						rotationalFound = 7;
					}
					
					/*
					 * Changed if statement for version 1.1 on 3/3/06
					 */
					if(rotationalFound >= 0 && this.visitedStates[i] != null)
					{
						found = true;
						//newValue = oldValue + stepSize * ( REWARD - oldValue);
						//check for winning state for more optimize update
						if(result == GameResult.Win)
						{
							try
							{
								if(this.visitedStates[i+1] == null)//changed to == might have been an error to have !=
								{
									this.states[1,j] = WinReward.ToString();
								}
								else
								{
									this.states[1,j] = Convert.ToString(double.Parse(states[1,j]) + StepSize * (WinReward - double.Parse(states[1,j])));
								}
							}
							catch(System.IndexOutOfRangeException)
							{
								this.states[1,j] = Convert.ToString(double.Parse(states[1,j]) + StepSize * (WinReward - double.Parse(states[1,j])));
							}
						}
						else if(result == GameResult.Tie)
						{
							try
							{
								if(i == MAX_STATES || this.visitedStates[i+1] == null )
								{
									this.states[1,j] = TieReward.ToString();
								}
								else
								{
									this.states[1,j] = Convert.ToString(double.Parse(states[1,j]) + StepSize * (TieReward - double.Parse(states[1,j])));
								}
							}
							catch(System.IndexOutOfRangeException)
							{
                                this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (TieReward - double.Parse(states[1, j])));
							}
						}
						else //result == -1
						{
							try
							{
								if(i == MAX_STATES || this.visitedStates[i+1] == null)
								{
									this.states[1,j] = LostReward.ToString();
								}
								else
								{
									this.states[1,j] = Convert.ToString(double.Parse(states[1,j]) + StepSize * (LostReward - double.Parse(states[1,j])));
								}
							}
							catch(System.IndexOutOfRangeException)
							{
                                this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (LostReward - double.Parse(states[1, j])));
							}
						}
						j = MAX; //Exit loop
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

                    this.states[0, location] = this.visitedStates[i];
                    
                    int reward = 0;

                    switch (result)
                    {
                        case GameResult.Win:
                            reward = WinReward;
                            break;
                        case GameResult.Tie:
                            reward = TieReward;
                            break;
                        case GameResult.Lost:
                            reward = LostReward;
                            break;
                    }
                    this.states[1, location] = Convert.ToString(this.defaultValue + StepSize * (reward - this.defaultValue));
				}
			}

			//write to xml
			XmlTextWriter MCstates = new XmlTextWriter("MCstates.xml",System.Text.Encoding.Default);
			MCstates.WriteStartDocument(true);
			MCstates.WriteStartElement("MonteCarlo");
			int inc = 0;
			while(inc< MAX && states[0,inc] != null)
			{
				MCstates.WriteStartElement("state");
				MCstates.WriteAttributeString("string",states[0,inc]);
				MCstates.WriteString(states[1,inc]);
				MCstates.WriteEndElement();
				inc++;
			}
			MCstates.WriteEndElement();
			MCstates.WriteEndDocument();
			MCstates.Close();
		}

		//find the best move and return it's answer.
		public int getMove(int[] board)
		{
			double [] values = new double[BOARD_SIZE];
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				values[i] = -100;
			}

			//Is autoExplore on?
			if(this.AutoExplore)
			{
				defaultValue = this.outOfBoundsReward;
			}
			else
			{
				//Set the default value to the average of the rewards
				// Ex: 1+0+(-1) = 0/3=0  
				//     0+(-1)+(-2) = -3/3=-1   
				//     1+(-1)+(-1) = -1/3 = -.333
				defaultValue = (this.WinReward + this.TieReward + this.LostReward)/3;
			}

			//Find Best Move by Value
			this.largestValue = double.MinValue;

			/*
			 * Changed for optimization 
			 * for version 1.1 on 3/3/06
			 * 
			 * Instead of string state;
			 * replaced it with
			 * 
			 * string state1;
			 * string state2;
			 * string state3;
			 * string state4;...
			 * 
			 * to represent the four rotations of the tic-tac-toe board
			 */
			string state1;
			string state2;
			string state3;
			string state4;
			
			string state5;
			string state6;
			string state7;
			string state8;

			//for each position on the board loop through
			for(int i = 0; i<BOARD_SIZE; i++)
			{
				/*
				 * Changed on 3/3/06 
				 * for optimization for version 1.1
				 * (see above for changes made)
				 * 
				 */
				state1 = "";
				state2 = "";
				state3 = "";
				state4 = "";
				state5 = "";
				state6 = "";
				state7 = "";
				state8 = "";

				//If current board position is empty
				if(board[i] == 0)
				{
					//If the current position is not the position currently viewing 
					//possible changes for then leave as is, else substitute the
					//current players value.
					for(int k = 0; k<BOARD_SIZE; k++)
					{
						if(i != k)
						{
							/*
							 * Version 1.1 Change
							 */
							//Leave default as first
							state1 = state1.Insert(state1.Length,board[k].ToString());
						}
						else
						{
							/*
							 * Version 1.1 Change
							 */
							state1 = state1.Insert(state1.Length,((int)this.player).ToString());
						}
					}
					
					//Version 1.1
					//Make the state2,state3,state4		
					state2 = (state1[2].ToString()+ state1[5].ToString() + state1[8].ToString() + state1[1].ToString() + state1[4].ToString() + state1[7].ToString() + state1[0].ToString() + state1[3].ToString() + state1[6].ToString());
					state3 = (state1[8].ToString()+ state1[7].ToString() + state1[6].ToString() + state1[5].ToString() + state1[4].ToString() + state1[3].ToString() + state1[2].ToString() + state1[1].ToString() + state1[0].ToString());
					state4 = (state1[6].ToString()+ state1[3].ToString() + state1[0].ToString() + state1[7].ToString() + state1[4].ToString() + state1[1].ToString() + state1[8].ToString() + state1[5].ToString() + state1[2].ToString());


					state5 = (state1[6].ToString()+ state1[7].ToString() + state1[8].ToString() + state1[3].ToString() + state1[4].ToString() + state1[5].ToString() + state1[0].ToString() + state1[1].ToString() + state1[2].ToString());
					state6 = (state1[2].ToString()+ state1[1].ToString() + state1[0].ToString() + state1[5].ToString() + state1[4].ToString() + state1[3].ToString() + state1[8].ToString() + state1[7].ToString() + state1[6].ToString());
					state7 = (state1[8].ToString()+ state1[5].ToString() + state1[2].ToString() + state1[7].ToString() + state1[4].ToString() + state1[1].ToString() + state1[6].ToString() + state1[3].ToString() + state1[0].ToString());
					state8 = (state1[0].ToString()+ state1[3].ToString() + state1[6].ToString() + state1[1].ToString() + state1[4].ToString() + state1[7].ToString() + state1[2].ToString() + state1[5].ToString() + state1[8].ToString());


					//find
					bool found = false;
					for(int j = 0; j< MAX; j++)
					{
						if(state1 == this.states[0,j])
						{
							found = true;
							//get the value
							values[i] = double.Parse(this.states[1,j]);
							if(values[i] > largestValue && board[i]==0)
							{
								largestValue = values[i];
								this.move = i;
							}
						}
						else if(state2 == this.states[0,j])
						{
							found = true;
							//get the value
							values[i] = double.Parse(this.states[1,j]);
							if(values[i] > largestValue && board[i]==0)
							{
								largestValue = values[i];
								this.move = i;
							}
						}
						else if(state3 == this.states[0,j])
						{
							found = true;
							//get the value
							values[i] = double.Parse(this.states[1,j]);
							if(values[i] > largestValue && board[i]==0)
							{
								largestValue = values[i];
								this.move = i;
							}
						}
						else if(state4 == this.states[0,j])
						{
							found = true;
							//get the value
							values[i] = double.Parse(this.states[1,j]);
							if(values[i] > largestValue && board[i]==0)
							{
								largestValue = values[i];
								this.move = i;
							}
						}
					}
					if(found == false)
					{
						if(defaultValue > largestValue)
						{
							largestValue = defaultValue;
							this.move = i;
						}
						values[i] = defaultValue;
					}
				}

			}

			//Check Epsilon Range
			double range = largestValue - this.Epsilon;
			Random randInt = new Random();
			//Find values within range and randomly select among them
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				if(values[i] >= range && move != i && board[i]==0)
				{
					//Randomly select between the two
					if(Math.Round(randInt.NextDouble(),0) == 1)  //then change to new
					{
						//set move
						move = i;
					}
				}
			}	


			/*
			 * Optimized update for version 1.1
			 * 
			 */
			state1 = "";
			state2 = "";
			state3 = "";
			state4 = "";
			state5 = "";
			state6 = "";
			state7 = "";
			state8 = "";

			this.visitedStates[moves] = "";
			int p;
			p = 0;
			try
			{
				for(; p< BOARD_SIZE; p++)
				{
					if(p == move && board[p]==0)
					{
						this.visitedStates[moves] = this.visitedStates[moves].Insert(this.visitedStates[moves].Length,((int)this.player).ToString());
					}
					else
					{
						this.visitedStates[moves] = this.visitedStates[moves].Insert(this.visitedStates[moves].Length,board[p].ToString());
					}
				}
			}
			catch(Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e+"\n"+"BOARD_SIZE "+BOARD_SIZE+"\np "+p+"\nmoves "+moves);
			}

			moves++;
			return move;
		}
	}
}
