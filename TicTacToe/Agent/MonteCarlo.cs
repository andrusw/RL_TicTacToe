using System;
using System.Xml;
using TicTacToe.Game;
using System.Collections.Generic;

namespace TicTacToe.Agent
{
	public class MonteCarlo : TicTacToeAgent
    {
		//Keep track for deep backup
		private string[] visitedStates = new string[MAX_STATES];

        /// <summary>
        /// Monte Carlo Constructor
        /// </summary>
        /// <param name="player"></param>
		public MonteCarlo(Player player)
		{
			this.player = player;

			//Read in the configuration
            ReadConfigFile("MCConfig.xml", "MonteCarlo");

            //Read in states
            ReadStates("MCstates.xml", "MonteCarlo");
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
                UpdateStates("MCstates.xml", "MonteCarlo");
			}

			bool found;
			//update values
			//for each of the moves
			for(int i = 0; i< MAX_STATES; i++)
			{
				found = false;
				//search the list to see if the state is there
                for (int j = 0; j < MAX; j++)
                {
                    if (this.visitedStates[i] != null)
                    {
                        List<string> mirrorStates = getAlternativeStates(this.visitedStates[i]);

                        if (mirrorStates.Exists(n => n == this.states[0, j]))
                        {
                            found = true;
                            //newValue = oldValue + stepSize * ( REWARD - oldValue);
                            //check for winning state for more optimize update
                            if (result == GameResult.Win)
                            {
                                try
                                {
                                    if (this.visitedStates[i + 1] == null)//changed to == might have been an error to have !=
                                    {
                                        this.states[1, j] = WinReward.ToString();
                                    }
                                    else
                                    {
                                        this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (WinReward - double.Parse(states[1, j])));
                                    }
                                }
                                catch (System.IndexOutOfRangeException)
                                {
                                    this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (WinReward - double.Parse(states[1, j])));
                                }
                            }
                            else if (result == GameResult.Tie)
                            {
                                try
                                {
                                    if (i == MAX_STATES || this.visitedStates[i + 1] == null)
                                    {
                                        this.states[1, j] = TieReward.ToString();
                                    }
                                    else
                                    {
                                        this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (TieReward - double.Parse(states[1, j])));
                                    }
                                }
                                catch (System.IndexOutOfRangeException)
                                {
                                    this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (TieReward - double.Parse(states[1, j])));
                                }
                            }
                            else //result == -1
                            {
                                try
                                {
                                    if (i == MAX_STATES || this.visitedStates[i + 1] == null)
                                    {
                                        this.states[1, j] = LostReward.ToString();
                                    }
                                    else
                                    {
                                        this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (LostReward - double.Parse(states[1, j])));
                                    }
                                }
                                catch (System.IndexOutOfRangeException)
                                {
                                    this.states[1, j] = Convert.ToString(double.Parse(states[1, j]) + StepSize * (LostReward - double.Parse(states[1, j])));
                                }
                            }
                            break;
                        }

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

            UpdateStates("MCstates.xml", "MonteCarlo");
		}

        /// <summary>
        /// Find the best move and return it's answer.
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
		public int getMove(int[] board)
		{
			double [] values = new double[BOARD_SIZE];

            //initialize the values to a low number
			for(int i = 0; i < BOARD_SIZE; i++)
			{
                values[i] = double.MinValue;
			}

            SetDefaultValue();

            values = FindMovesWinPercentage(board, values);

            if (AutoExplore)
            {
                CheckEpsilonRange(board, values);
            }

			this.visitedStates[moves] = "";
			
            
			try
			{
				for(int p = 0; p < BOARD_SIZE; p++)
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
				System.Windows.Forms.MessageBox.Show(e.ToString());
			}

			moves++;
			return move;
		}

        /// <summary>
        /// Reset seen states when a new game starts.
        /// </summary>
        public void Reset()
        {
            visitedStates = new string[MAX_STATES];
            moves = 0;
        }
	}
}
