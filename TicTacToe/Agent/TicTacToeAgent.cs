using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Game;
using System.Xml;

namespace TicTacToe.Agent
{
    public abstract class TicTacToeAgent
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
        protected const int MAX_STATES = 5;
        protected const int BOARD_SIZE = 9;
        protected const int MAX = 800; 
        #endregion

        protected int outOfBoundsReward;
        protected double largestValue = Double.MinValue;
        protected int move;//represents the decision of where to move
        protected int moves = 0;//represents the number of moves made;
        protected Player player;
        protected double defaultValue = 2.0;
        protected string[,] states = new string[2, MAX];//[state,value] x MAX

        public TicTacToeAgent()
        {
            outOfBoundsReward = WinReward + 1;
        }

        /// <summary>
        /// Find states that are considered similar to the current state.
        /// --Horizontally, Vertically, Mirror, Diagonally
        /// </summary>
        /// <param name="currentState"></param>
        /// <returns></returns>
        protected List<string> getAlternativeStates(string currentState)
        {
            List<string> states = new List<string>();
            states.Add(currentState);
            states.Add((currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString()));
            states.Add((currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString()));
            states.Add((currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString()));
            states.Add((currentState[6].ToString() + currentState[7].ToString() + currentState[8].ToString() + currentState[3].ToString() + currentState[4].ToString() + currentState[5].ToString() + currentState[0].ToString() + currentState[1].ToString() + currentState[2].ToString()));
            states.Add((currentState[2].ToString() + currentState[1].ToString() + currentState[0].ToString() + currentState[5].ToString() + currentState[4].ToString() + currentState[3].ToString() + currentState[8].ToString() + currentState[7].ToString() + currentState[6].ToString()));
            states.Add((currentState[8].ToString() + currentState[5].ToString() + currentState[2].ToString() + currentState[7].ToString() + currentState[4].ToString() + currentState[1].ToString() + currentState[6].ToString() + currentState[3].ToString() + currentState[0].ToString()));
            states.Add((currentState[0].ToString() + currentState[3].ToString() + currentState[6].ToString() + currentState[1].ToString() + currentState[4].ToString() + currentState[7].ToString() + currentState[2].ToString() + currentState[5].ToString() + currentState[8].ToString()));

            return states;
        }

        /// <summary>
        /// Read configuration file and returns settings
        /// </summary>
        /// <param name="xml">XML Name</param>
        /// <param name="startElement">Root Element Name</param>
        protected void ReadConfigFile(string xml, string startElement)
        {
            /*Set configuration default values*/
            Epsilon = 0.1;
            StepSize = 0.1;
            WinReward = 1;
            TieReward = 0;
            LostReward = -1;
            AutoExplore = true;

            try
            {
                using (XmlTextReader agentConfig = new XmlTextReader(xml))
                {
                    agentConfig.ReadStartElement(startElement);
                    agentConfig.ReadStartElement("Epsilon");
                    Epsilon = double.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Stepsize");
                    StepSize = double.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Reward");
                    agentConfig.ReadStartElement("Win");
                    WinReward = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Tie");
                    TieReward = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Loss");
                    LostReward = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("AutoExplore");
                    string ans = agentConfig.ReadString();
                    if (ans == "Yes")
                    {
                        AutoExplore = true;
                    }
                    else //ans == no
                    {
                        AutoExplore = false;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                //do nothing, use defaults
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Read States From XML
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="startElement"></param>
        protected void ReadStates(string xml, string startElement)
        {
            try
            {
                using (XmlTextReader agentStates = new XmlTextReader(xml))
                {
                    agentStates.ReadStartElement(startElement);
                    int i = 0;
                    this.states[0, i] = agentStates.GetAttribute("string");
                    this.states[1, i] = agentStates.ReadString();
                    i++;
                    while (agentStates.Read())
                    {
                        this.states[0, i] = agentStates.GetAttribute("string");
                        this.states[1, i] = agentStates.ReadString();
                        i++;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                //do nothing, use defaults
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Set default value of reward
        /// </summary>
        protected void SetDefaultValue()
        {
            //Is autoExplore on?
            if (this.AutoExplore)
            {
                defaultValue = this.outOfBoundsReward;
            }
            else
            {
                //Set the default value to the average of the rewards
                // Ex: 1+0+(-1) = 0/3=0  
                //     0+(-1)+(-2) = -3/3=-1   
                //     1+(-1)+(-1) = -1/3 = -.333
                defaultValue = (this.WinReward + this.TieReward + this.LostReward) / 3;
            }
        }

        /// <summary>
        /// Find the next best move
        /// </summary>
        protected double[] FindMovesWinPercentage(int[] board, double[] values)
        {
            this.largestValue = double.MinValue;

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                string possibleState = "";
                if (board[i] == 0)//No player has moved there yet.
                {
                    //make string value of the possible state
                    for (int k = 0; k < BOARD_SIZE; k++)
                    {
                        if (i != k)
                        {
                            possibleState = possibleState.Insert(possibleState.Length, board[k].ToString());
                        }
                        else
                        {
                            possibleState = possibleState.Insert(possibleState.Length, ((int)this.player).ToString());
                        }
                    }

                    List<string> mirrorStates = getAlternativeStates(possibleState);

                    //find that state
                    bool found = false;
                    for (int j = 0; j < MAX; j++)
                    {
                        if (mirrorStates.Exists(n => n == this.states[0, j]))
                        {
                            found = true;
                            values[i] = double.Parse(this.states[1, j]);
                            if (values[i] > largestValue && board[i] == 0)
                            {
                                largestValue = values[i];
                                this.move = i;
                            }
                        }
                    }
                    if (found == false)
                    {
                        if (defaultValue > largestValue)
                        {
                            largestValue = defaultValue;
                            this.move = i;
                        }
                        values[i] = defaultValue;
                    }
                }
            }

            return values;
        }

        /// <summary>
        /// Find Values within range of epsilon
        /// </summary>
        protected void CheckEpsilonRange(int[] board, double[] values)
        {
            //Check Epsilon Range
            double range = largestValue - this.Epsilon;

            Random randInt = new Random();
            //Find values within range and randomly select among them
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                if (values[i] >= range && move != i && board[i] == 0)
                {
                    //Randomly select between the two
                    if (Math.Round(randInt.NextDouble(), 0) == 1)  //then change to new
                    {
                        //set move
                        move = i;
                    }
                }
            }
        }

        /// <summary>
        /// Update States XML
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="startElement"></param>
        protected void UpdateStates(string xml, string startElement)
        {
            try
            {
                using (XmlTextReader agentStatesRead = new XmlTextReader(xml))
                {
                    agentStatesRead.ReadStartElement(startElement);
                    int i = 0;
                    this.states[0, i] = agentStatesRead.GetAttribute("string");
                    this.states[1, i] = agentStatesRead.ReadString();
                    i++;
                    while (agentStatesRead.Read())
                    {
                        this.states[0, i] = agentStatesRead.GetAttribute("string");
                        this.states[1, i] = agentStatesRead.ReadString();
                        i++;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                //do nothing, use defaults
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

    }
}
