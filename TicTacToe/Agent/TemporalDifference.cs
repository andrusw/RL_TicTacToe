using System;
using System.Xml;
using TicTacToe.Game;

namespace TicTacToe.Agent
{
	public class TemporalDifference
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
		private double largestValue = -2.0;
		private int move;//represents the decision of where to move
		private int moves = 0;//represents the number of moves made;
		//Read/Write states in the array/file
		private const int MAX = 800;
		private string [,] states = new string[2,MAX];
		private Player player;
		private double defaultValue = 2.0;
		private string previousState = "";

		public TemporalDifference(Player player)
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
			 * 
			 */ 
			try
			{
				XmlTextReader TDconfig = new XmlTextReader("TDConfig.xml");
				
				TDconfig.ReadStartElement("TemporalDifference");
				TDconfig.ReadStartElement("Epsilon");
				Epsilon = double.Parse(TDconfig.ReadString());
				TDconfig.ReadEndElement();
				TDconfig.ReadStartElement("Stepsize");
				StepSize = double.Parse(TDconfig.ReadString());
				TDconfig.ReadEndElement();
				TDconfig.ReadStartElement("Reward");
				TDconfig.ReadStartElement("Win");
				WinReward = int.Parse(TDconfig.ReadString());
				TDconfig.ReadEndElement();
				TDconfig.ReadStartElement("Tie");
				TieReward = int.Parse(TDconfig.ReadString());
				TDconfig.ReadEndElement();
				TDconfig.ReadStartElement("Loss");
				LostReward = int.Parse(TDconfig.ReadString());
				TDconfig.ReadEndElement();
				TDconfig.ReadEndElement();
				TDconfig.ReadStartElement("AutoExplore");
				string ans = TDconfig.ReadString();
				if(ans == "Yes")
				{
					AutoExplore = true;
				}
				else //ans == no
				{
					AutoExplore = false;
				}
				TDconfig.Close();

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
			 * <DynamicProgramming>
			 * <state string="?????????">value</state>
			 * </MonteCarlo>
			 * </xml>
			 */ 
			try
			{
				XmlTextReader TDstates = new XmlTextReader("TDstates.xml");
				TDstates.ReadStartElement("TemporalDifference");
				int i = 0;
				this.states[0,i] = TDstates.GetAttribute("string");
				this.states[1,i] = TDstates.ReadString();
				i++;
				while(TDstates.Read())
				{
					this.states[0,i] = TDstates.GetAttribute("string");
					this.states[1,i] = TDstates.ReadString();
					i++;
				}
				TDstates.Close();
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
		//find the best move and return it's answer.
		public void innerUpdate(double newValue, string currentState)
		{
			bool found = false;

			/*
			 * Optimized update for version 1.1 on 3/3/06
			 * 
			 * 
			 */
			string currentState2 = (currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString());
			string currentState3 = (currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString());
			string currentState4 = (currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString());

			string currentState5 = (currentState[6].ToString() + currentState[7].ToString() + currentState[8].ToString() + currentState[3].ToString() + currentState[4].ToString() + currentState[5].ToString() + currentState[0].ToString() + currentState[1].ToString() + currentState[2].ToString());
			string currentState6 = (currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString());
			string currentState7 = (currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString());
			string currentState8 = (currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString());

			for(int j = 0; j< MAX; j++)
			{
				/*
				 * Optimized update for version 1.1 on 3/3/06
				 */
				if(currentState == this.states[0,j] || currentState2 == this.states[0,j] || currentState3 == this.states[0,j] || currentState4 == this.states[0,j] || currentState5 == this.states[0,j] || currentState6 == this.states[0,j] || currentState7 == this.states[0,j] || currentState8 == this.states[0,j])
				{
					found = true;
					this.states[1,j] = Convert.ToString(double.Parse(this.states[1,j]) + this.StepSize*(newValue - double.Parse(this.states[1,j])));
					j = MAX; //Exit loop
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

		//find the best move and return it's answer.
		public int getMove(int[] board)
		{
			string currentState = "";
			double newValue = 0.0;
			double [] values = new double[9];

			//initialize the values to a low number
			for(int i = 0; i < BOARD_SIZE; i++)
			{
				values[i] = -100;
				currentState = currentState.Insert(currentState.Length,board[i].ToString());
			}

			//Is autoExplore on?
			if(this.AutoExplore)
			{
				defaultValue = this.outOfBoundsReward;
			}
			else
			{
				// 1+0+(-1) = 0/3=0   0+(-1)+(-2) = -3/3=-1   1+(-1)+(-1) = -1/3 = -.333
				defaultValue = (this.WinReward + this.TieReward + this.LostReward)/3;
			}
			

			//Find Best Move by Value
            this.largestValue = double.MinValue;
			string state;
			for(int i = 0; i<BOARD_SIZE; i++)
			{
				state = "";
				if(board[i] == 0)
				{
					//make string value of a possible state
					for(int k = 0; k<BOARD_SIZE; k++)
					{
						if(i != k)
						{
							state = state.Insert(state.Length,board[k].ToString());
						}
						else
						{
                            state = state.Insert(state.Length, ((int)this.player).ToString());
						}
					}

					/*
					 * Optimized update for version 1.1 on 3/3/06
					 *  
					 */
					string state2 = (state[2].ToString() + state[5].ToString() + state[8].ToString() + state[1].ToString() + state[4].ToString() + state[7].ToString() + state[0].ToString() + state[3].ToString() + state[6].ToString());
					string state3 = (state[8].ToString() + state[7].ToString() + state[6].ToString() + state[5].ToString() + state[4].ToString() + state[3].ToString() + state[2].ToString() + state[1].ToString() + state[0].ToString());
					string state4 = (state[6].ToString() + state[3].ToString() + state[0].ToString() + state[7].ToString() + state[4].ToString() + state[1].ToString() + state[8].ToString() + state[5].ToString() + state[2].ToString());

					string state5 = (state[6].ToString() + state[7].ToString() + state[8].ToString() + state[3].ToString() + state[4].ToString() + state[5].ToString() + state[0].ToString() + state[1].ToString() + state[2].ToString());
					string state6 = (state[2].ToString() + state[1].ToString() + state[0].ToString() + state[5].ToString() + state[4].ToString() + state[3].ToString() + state[8].ToString() + state[7].ToString() + state[6].ToString());
					string state7 = (state[8].ToString() + state[5].ToString() + state[2].ToString() + state[7].ToString() + state[4].ToString() + state[1].ToString() + state[6].ToString() + state[3].ToString() + state[0].ToString());
					string state8 = (state[0].ToString() + state[3].ToString() + state[6].ToString() + state[1].ToString() + state[4].ToString() + state[7].ToString() + state[2].ToString() + state[5].ToString() + state[8].ToString());



					//find that state
					bool found = false;
					for(int j = 0; j< MAX; j++)
					{
						if(state == this.states[0,j] || state2 == this.states[0,j] || state3 == this.states[0,j] || state4 == this.states[0,j] || state5 == this.states[0,j]|| state6 == this.states[0,j]|| state7 == this.states[0,j]|| state8 == this.states[0,j])
						{
							found = true;
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

			newValue = largestValue;

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
						newValue = values[i];
					}
				}
			}	


			state = "";

			moves++;
			board[move] = (int)this.player;
			if(this.previousState != "")
			{
				innerUpdate(newValue, this.previousState);
			}
			this.previousState = "";
			for(int i = 0; i< 9; i++)
			{
				this.previousState = this.previousState.Insert(this.previousState.Length,board[i].ToString());
			}

			return move;
		}
		//update the xml file after the game
        public void update(GameResult result, int[] board, GameType gameType)
		{
			string currentState = "";
			//Assuming Board_SIZE = 9
			for(int i = 0; i < 9; i++)
			{
				currentState = currentState.Insert(currentState.Length,board[i].ToString());
			}
			//Read in values
			try
			{
				XmlTextReader TDstatesRead = new XmlTextReader("TDstates.xml");
				TDstatesRead.ReadStartElement("TemporalDifference");
				int i = 0;
				this.states[0,i] = TDstatesRead.GetAttribute("string");
				this.states[1,i] = TDstatesRead.ReadString();
				i++;
				while(TDstatesRead.Read())
				{
					this.states[0,i] = TDstatesRead.GetAttribute("string");
					this.states[1,i] = TDstatesRead.ReadString();
					i++;
				}
				TDstatesRead.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}

			bool found;

			/*
			 * Optimized update for version 1.1 on 3/3/06
			 * 
			 */
			string currentState2 = (currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString());
			string currentState3 = (currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString());
			string currentState4 = (currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString());

			string currentState5 = (currentState[6].ToString() + currentState[7].ToString() + currentState[8].ToString() + currentState[3].ToString() + currentState[4].ToString() + currentState[5].ToString() + currentState[0].ToString() + currentState[1].ToString() + currentState[2].ToString());
			string currentState6 = (currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString());
			string currentState7 = (currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString());
			string currentState8 = (currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString());

			//update values
			found = false;
			for(int j = 0; j< MAX; j++)
			{
				if(currentState == this.states[0,j] || currentState2 == this.states[0,j] || currentState3 == this.states[0,j] || currentState4 == this.states[0,j] || currentState5 == this.states[0,j] || currentState6 == this.states[0,j] || currentState7 == this.states[0,j] || currentState8 == this.states[0,j])
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
