using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace TicTacToe.Configuration
{
    public abstract class ConfigForm : System.Windows.Forms.Form
    {
        #region Form Items
        protected System.Windows.Forms.Button buttonHelpOnEpsilon;
        protected System.Windows.Forms.Button buttonHelpOnStepSize;
        protected System.Windows.Forms.Button buttonHelpOnAutoExplore;
        protected System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.Button buttonHelpOnRewards;
        protected System.Windows.Forms.GroupBox groupBoxAutoExplore;
        protected System.Windows.Forms.GroupBox groupBoxRewards;
        protected System.Windows.Forms.RadioButton radioButtonNo;
        protected System.Windows.Forms.RadioButton radioButtonYes;
        protected System.Windows.Forms.Label labelLost;
        protected System.Windows.Forms.Label labelTie;
        protected System.Windows.Forms.Label labelWin;
        protected System.Windows.Forms.Label labelStep;
        protected System.Windows.Forms.Label labelEpsilon;
        protected System.Windows.Forms.NumericUpDown numericUpDownStep;
        protected System.Windows.Forms.NumericUpDown numericUpDownWin;
        protected System.Windows.Forms.NumericUpDown numericUpDownTie;
        protected System.Windows.Forms.NumericUpDown numericUpDownLost;
        protected System.Windows.Forms.NumericUpDown numericUpDownEpsilon;
        #endregion

        /// <summary>
        /// Write configuration settings to XML, used by agent
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="startElement"></param>
        protected void ReadConfig(string xml, string startElement)
        {
            try
            {
                using (XmlTextReader agentConfig = new XmlTextReader(xml))
                {
                    agentConfig.ReadStartElement(startElement);
                    agentConfig.ReadStartElement("Epsilon");
                    this.numericUpDownEpsilon.Value = decimal.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Stepsize");
                    if (this.numericUpDownStep != null)
                    {
                        this.numericUpDownStep.Value = decimal.Parse(agentConfig.ReadString());
                        agentConfig.ReadEndElement();
                    }
                    agentConfig.ReadStartElement("Reward");
                    agentConfig.ReadStartElement("Win");
                    this.numericUpDownWin.Value = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Tie");
                    this.numericUpDownTie.Value = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("Loss");
                    this.numericUpDownLost.Value = int.Parse(agentConfig.ReadString());
                    agentConfig.ReadEndElement();
                    agentConfig.ReadEndElement();
                    agentConfig.ReadStartElement("AutoExplore");
                    string ans = agentConfig.ReadString();
                    if (ans == "Yes")
                    {
                        this.radioButtonYes.Checked = true;
                        this.radioButtonNo.Checked = false;
                    }
                    else //ans == no
                    {
                        this.radioButtonYes.Checked = false;
                        this.radioButtonNo.Checked = true;
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                FileInfo fi = new FileInfo(xml);
                fi.Create();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Write configuration file
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="startElement"></param>
        /// <param name="name"></param>
        protected void WriteConfig(string xml, string startElement, string name)
        {
            try
            {
                using (XmlTextWriter agentConfig = new XmlTextWriter(xml, System.Text.Encoding.Default))
                {
                    agentConfig.WriteStartDocument(true);
                    agentConfig.WriteComment("This xml is used to configure the " + name + " learning");
                    agentConfig.WriteStartElement(startElement);
                    agentConfig.WriteStartElement("Epsilon");
                    agentConfig.WriteString(this.numericUpDownEpsilon.Value.ToString());
                    agentConfig.WriteEndElement();
                    agentConfig.WriteStartElement("Stepsize");
                    if (this.numericUpDownStep != null)
                    {
                        agentConfig.WriteString(this.numericUpDownStep.Value.ToString());
                    }
                    agentConfig.WriteEndElement();   
                    agentConfig.WriteStartElement("Reward");
                    agentConfig.WriteStartElement("Win");
                    agentConfig.WriteString(this.numericUpDownWin.Value.ToString());
                    agentConfig.WriteEndElement();
                    agentConfig.WriteStartElement("Tie");
                    agentConfig.WriteString(this.numericUpDownTie.Value.ToString());
                    agentConfig.WriteEndElement();
                    agentConfig.WriteStartElement("Loss");
                    agentConfig.WriteString(this.numericUpDownLost.Value.ToString());
                    agentConfig.WriteEndElement();
                    agentConfig.WriteEndElement();
                    agentConfig.WriteStartElement("AutoExplore");
                    if (this.radioButtonYes.Checked)
                    {
                        agentConfig.WriteString("Yes");
                    }
                    else
                    {
                        agentConfig.WriteString("No");
                    }
                    agentConfig.WriteEndElement();
                    agentConfig.WriteEndElement();
                    agentConfig.WriteEndDocument();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Epsilon Help Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonHelpOnEpsilon_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("EPSILON is used for exploitation to set a range of acceptable values.\n" +
                "If there are 2 or more values that are within a acceptable \"range\"" +
                "then the agent will randomly select among those\n" +
                "eg.  State1 = .9, State2 = .8, State3 = .79\n" +
                "with epsilon at 0.1 it would look at anything within 0.9 - 0.1 = 0.8\n" +
                "\nThe Maximum value of epsilon is 1.0, in this case the possible states are randomly selected.\n" +
                "The Minimum value of epsilon is 0.0, in this case only the highest value would be selected.", "Epsilon Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /// <summary>
        /// Rewards Help Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonHelpOnRewards_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("The \"win\" represents the reward or punishment you will give the agent anytime it wins\n" +
                "The \"tie\" represents the reward or punishment you will give the agent when the game is a tie\n" +
                "The \"lost\" represents the reward or punishment you will give the agent anytime it loses\n" +
                "\nPlaying around with these values can change how well and quickly the agent may learn\n" +
                "By default the agent is given a +1 for a win, a 0 for a tie, and a -1 for a lost\n" +
                "However, some other interesting rewards may be\n" +
                "win: +1, tie: -1, lost:  -1   Here anything but a win is only satisfactory\n" +
                "win: 0, tie: -1, lost: -2     Here the agent learns to fear ties and lost, and receives no positive reinforcement\n"
                , "Help on Rewards", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Help on Auto Explore Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonHelpOnAutoExplore_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Automatic exploration of new states will allow your agent to learn\n" +
                "more quickly, by visiting states new states no matter what the other\n" +
                "values of the visited states might be. The more states that the agent\n" +
                "knows of, the smarter it will be.", "Information on Automatic Exploration of New States", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Step Size Help Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonHelpOnStepSize_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("The smaller the STEP SIZE the smarter the agent will play\n" +
                "but then the agent will need to play more games. (Takes a longer time to learn)\n" +
                "I recommended to use a higher STEPSIZE like 0.1 in the beginning of learning\n" +
                "then change the step size to a lower value later on.\n" +
                "\nTry 0.1 (Ten visits per state to get a decent value) then\n" +
                "Try 0.01 (One Hundred visits per state) then\n" +
                "Try 0.001 (1000 visits/state) and so on\n" +
                "\nThe step size value deals with the updating of the state's value\n" +
                "The formula is as follow\n" +
                "newValue = oldValue + stepSize * ( REWARD - oldValue)", "Step Size Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
