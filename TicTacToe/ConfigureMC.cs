using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe.Agent
{
	/// <summary>
	/// Summary description for ConfigureMC.
	/// </summary>
	public class ConfigureMC : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelEpsilon;
		private System.Windows.Forms.NumericUpDown numericUpDownEpsilon;
		private System.Windows.Forms.Button buttonHelpOnEpsilon;
		private System.Windows.Forms.Label labelStep;
		private System.Windows.Forms.NumericUpDown numericUpDownStep;
		private System.Windows.Forms.Button buttonHelpOnStepSize;
		private System.Windows.Forms.Label labelWin;
		private System.Windows.Forms.Label labelTie;
		private System.Windows.Forms.Label labelLost;
		private System.Windows.Forms.NumericUpDown numericUpDownWin;
		private System.Windows.Forms.NumericUpDown numericUpDownTie;
		private System.Windows.Forms.NumericUpDown numericUpDownLost;
		private System.Windows.Forms.Button buttonHelpOnRewards;
		private System.Windows.Forms.GroupBox groupBoxRewards;
		private System.Windows.Forms.GroupBox groupBoxAutoExploreNewStates;
		private System.Windows.Forms.RadioButton radioButtonYes;
		private System.Windows.Forms.RadioButton radioButtonNo;
		private System.Windows.Forms.Button buttonHelpOnAutoExplore;
		private System.Windows.Forms.Button buttonOK;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ConfigureMC()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			try
			{
				XmlTextReader MCconfig = new XmlTextReader("MCConfig.xml");
				
				MCconfig.ReadStartElement("MonteCarlo");
				MCconfig.ReadStartElement("Epsilon");
				this.numericUpDownEpsilon.Value = decimal.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Stepsize");
				this.numericUpDownStep.Value = decimal.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Reward");
				MCconfig.ReadStartElement("Win");
				this.numericUpDownWin.Value = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Tie");
				this.numericUpDownTie.Value = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("Loss");
				this.numericUpDownLost.Value = int.Parse(MCconfig.ReadString());
				MCconfig.ReadEndElement();
				MCconfig.ReadEndElement();
				MCconfig.ReadStartElement("AutoExplore");
				string ans = MCconfig.ReadString();
				if(ans == "Yes")
				{
					this.radioButtonYes.Checked = true;
					this.radioButtonNo.Checked = false;
				}
				else //ans == no
				{
					this.radioButtonYes.Checked = false;
					this.radioButtonNo.Checked = true;
				}
				MCconfig.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				MessageBox.Show(err.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureMC));
            this.labelEpsilon = new System.Windows.Forms.Label();
            this.numericUpDownEpsilon = new System.Windows.Forms.NumericUpDown();
            this.buttonHelpOnEpsilon = new System.Windows.Forms.Button();
            this.labelStep = new System.Windows.Forms.Label();
            this.numericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.buttonHelpOnStepSize = new System.Windows.Forms.Button();
            this.labelWin = new System.Windows.Forms.Label();
            this.labelTie = new System.Windows.Forms.Label();
            this.labelLost = new System.Windows.Forms.Label();
            this.numericUpDownWin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTie = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLost = new System.Windows.Forms.NumericUpDown();
            this.buttonHelpOnRewards = new System.Windows.Forms.Button();
            this.groupBoxRewards = new System.Windows.Forms.GroupBox();
            this.groupBoxAutoExploreNewStates = new System.Windows.Forms.GroupBox();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.buttonHelpOnAutoExplore = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLost)).BeginInit();
            this.groupBoxRewards.SuspendLayout();
            this.groupBoxAutoExploreNewStates.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelEpsilon
            // 
            this.labelEpsilon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEpsilon.Location = new System.Drawing.Point(8, 16);
            this.labelEpsilon.Name = "labelEpsilon";
            this.labelEpsilon.Size = new System.Drawing.Size(64, 20);
            this.labelEpsilon.TabIndex = 0;
            this.labelEpsilon.Text = "Epsilon";
            // 
            // numericUpDownEpsilon
            // 
            this.numericUpDownEpsilon.DecimalPlaces = 4;
            this.numericUpDownEpsilon.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownEpsilon.Location = new System.Drawing.Point(104, 16);
            this.numericUpDownEpsilon.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEpsilon.Name = "numericUpDownEpsilon";
            this.numericUpDownEpsilon.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownEpsilon.TabIndex = 1;
            this.numericUpDownEpsilon.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // buttonHelpOnEpsilon
            // 
            this.buttonHelpOnEpsilon.Location = new System.Drawing.Point(192, 16);
            this.buttonHelpOnEpsilon.Name = "buttonHelpOnEpsilon";
            this.buttonHelpOnEpsilon.Size = new System.Drawing.Size(104, 20);
            this.buttonHelpOnEpsilon.TabIndex = 2;
            this.buttonHelpOnEpsilon.Text = "Help on Epsilon";
            this.buttonHelpOnEpsilon.Click += new System.EventHandler(this.buttonHelpOnEpsilon_Click);
            // 
            // labelStep
            // 
            this.labelStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStep.Location = new System.Drawing.Point(8, 40);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(64, 20);
            this.labelStep.TabIndex = 3;
            this.labelStep.Text = "Step Size";
            // 
            // numericUpDownStep
            // 
            this.numericUpDownStep.DecimalPlaces = 5;
            this.numericUpDownStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownStep.Location = new System.Drawing.Point(104, 40);
            this.numericUpDownStep.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            327680});
            this.numericUpDownStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownStep.Name = "numericUpDownStep";
            this.numericUpDownStep.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownStep.TabIndex = 4;
            this.numericUpDownStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // buttonHelpOnStepSize
            // 
            this.buttonHelpOnStepSize.Location = new System.Drawing.Point(192, 40);
            this.buttonHelpOnStepSize.Name = "buttonHelpOnStepSize";
            this.buttonHelpOnStepSize.Size = new System.Drawing.Size(104, 20);
            this.buttonHelpOnStepSize.TabIndex = 5;
            this.buttonHelpOnStepSize.Text = "Help on Step Size";
            this.buttonHelpOnStepSize.Click += new System.EventHandler(this.buttonHelpOnStepSize_Click);
            // 
            // labelWin
            // 
            this.labelWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWin.Location = new System.Drawing.Point(8, 24);
            this.labelWin.Name = "labelWin";
            this.labelWin.Size = new System.Drawing.Size(40, 20);
            this.labelWin.TabIndex = 0;
            this.labelWin.Text = "Win";
            // 
            // labelTie
            // 
            this.labelTie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTie.Location = new System.Drawing.Point(8, 48);
            this.labelTie.Name = "labelTie";
            this.labelTie.Size = new System.Drawing.Size(32, 20);
            this.labelTie.TabIndex = 2;
            this.labelTie.Text = "Tie";
            // 
            // labelLost
            // 
            this.labelLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLost.Location = new System.Drawing.Point(8, 72);
            this.labelLost.Name = "labelLost";
            this.labelLost.Size = new System.Drawing.Size(48, 20);
            this.labelLost.TabIndex = 4;
            this.labelLost.Text = "Lost";
            // 
            // numericUpDownWin
            // 
            this.numericUpDownWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownWin.Location = new System.Drawing.Point(96, 24);
            this.numericUpDownWin.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownWin.Name = "numericUpDownWin";
            this.numericUpDownWin.Size = new System.Drawing.Size(64, 22);
            this.numericUpDownWin.TabIndex = 1;
            this.numericUpDownWin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownTie
            // 
            this.numericUpDownTie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownTie.Location = new System.Drawing.Point(96, 48);
            this.numericUpDownTie.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownTie.Name = "numericUpDownTie";
            this.numericUpDownTie.Size = new System.Drawing.Size(64, 22);
            this.numericUpDownTie.TabIndex = 3;
            // 
            // numericUpDownLost
            // 
            this.numericUpDownLost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownLost.Location = new System.Drawing.Point(96, 72);
            this.numericUpDownLost.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownLost.Name = "numericUpDownLost";
            this.numericUpDownLost.Size = new System.Drawing.Size(64, 22);
            this.numericUpDownLost.TabIndex = 5;
            this.numericUpDownLost.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // buttonHelpOnRewards
            // 
            this.buttonHelpOnRewards.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHelpOnRewards.Location = new System.Drawing.Point(184, 48);
            this.buttonHelpOnRewards.Name = "buttonHelpOnRewards";
            this.buttonHelpOnRewards.Size = new System.Drawing.Size(104, 20);
            this.buttonHelpOnRewards.TabIndex = 6;
            this.buttonHelpOnRewards.Text = "Help on Rewards";
            this.buttonHelpOnRewards.Click += new System.EventHandler(this.buttonHelpOnRewards_Click);
            // 
            // groupBoxRewards
            // 
            this.groupBoxRewards.Controls.Add(this.labelLost);
            this.groupBoxRewards.Controls.Add(this.labelTie);
            this.groupBoxRewards.Controls.Add(this.labelWin);
            this.groupBoxRewards.Controls.Add(this.numericUpDownWin);
            this.groupBoxRewards.Controls.Add(this.numericUpDownTie);
            this.groupBoxRewards.Controls.Add(this.numericUpDownLost);
            this.groupBoxRewards.Controls.Add(this.buttonHelpOnRewards);
            this.groupBoxRewards.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRewards.Location = new System.Drawing.Point(8, 64);
            this.groupBoxRewards.Name = "groupBoxRewards";
            this.groupBoxRewards.Size = new System.Drawing.Size(312, 98);
            this.groupBoxRewards.TabIndex = 6;
            this.groupBoxRewards.TabStop = false;
            this.groupBoxRewards.Text = "Rewards";
            // 
            // groupBoxAutoExploreNewStates
            // 
            this.groupBoxAutoExploreNewStates.Controls.Add(this.radioButtonNo);
            this.groupBoxAutoExploreNewStates.Controls.Add(this.radioButtonYes);
            this.groupBoxAutoExploreNewStates.Controls.Add(this.buttonHelpOnAutoExplore);
            this.groupBoxAutoExploreNewStates.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAutoExploreNewStates.Location = new System.Drawing.Point(8, 168);
            this.groupBoxAutoExploreNewStates.Name = "groupBoxAutoExploreNewStates";
            this.groupBoxAutoExploreNewStates.Size = new System.Drawing.Size(312, 64);
            this.groupBoxAutoExploreNewStates.TabIndex = 7;
            this.groupBoxAutoExploreNewStates.TabStop = false;
            this.groupBoxAutoExploreNewStates.Text = "Auto Explore New States?";
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonNo.Location = new System.Drawing.Point(80, 32);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(56, 24);
            this.radioButtonNo.TabIndex = 1;
            this.radioButtonNo.Text = "No";
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.Checked = true;
            this.radioButtonYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonYes.Location = new System.Drawing.Point(16, 32);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(56, 24);
            this.radioButtonYes.TabIndex = 0;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "Yes";
            // 
            // buttonHelpOnAutoExplore
            // 
            this.buttonHelpOnAutoExplore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonHelpOnAutoExplore.Location = new System.Drawing.Point(176, 32);
            this.buttonHelpOnAutoExplore.Name = "buttonHelpOnAutoExplore";
            this.buttonHelpOnAutoExplore.Size = new System.Drawing.Size(128, 20);
            this.buttonHelpOnAutoExplore.TabIndex = 2;
            this.buttonHelpOnAutoExplore.Text = "Help on Auto Explore";
            this.buttonHelpOnAutoExplore.Click += new System.EventHandler(this.buttonHelpOnAutoExplore_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(240, 240);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 8;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ConfigureMC
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(328, 269);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxAutoExploreNewStates);
            this.Controls.Add(this.groupBoxRewards);
            this.Controls.Add(this.buttonHelpOnStepSize);
            this.Controls.Add(this.numericUpDownStep);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.buttonHelpOnEpsilon);
            this.Controls.Add(this.numericUpDownEpsilon);
            this.Controls.Add(this.labelEpsilon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureMC";
            this.Text = "Monte Carlo Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLost)).EndInit();
            this.groupBoxRewards.ResumeLayout(false);
            this.groupBoxAutoExploreNewStates.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        private void buttonHelpOnEpsilon_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("EPSILON is used for exploitation to set a range of acceptable values.\n"+
				            "If there are 2 or more values that are within a acceptable \"range\""+
				            "then the agent will randomly select among those\n"+ 
					        "eg.  State1 = .9, State2 = .8, State3 = .79\n"+
							"with epsilon at 0.1 it would look at anything within 0.9 - 0.1 = 0.8\n"+
				            "\nThe Maximum value of epsilon is 1.0, in this case the possible states are randomly selected.\n"+
				            "The Minimum value of epsilon is 0.0, in this case only the highest value would be selected.","Epsilon Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

        private void buttonHelpOnStepSize_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("The smaller the STEP SIZE the smarter the agent will play\n"+
							"but then the agent will need to play more games. (Takes a longer time to learn)\n"+
							"I recommended to use a higher STEPSIZE like 0.1 in the beginning of learning\n"+
				            "then change the step size to a lower value later on.\n"+ 
							"\nTry 0.1 (Ten visits per state to get a decent value) then\n"+
							"Try 0.01 (One Hundred visits per state) then\n"+
							"Try 0.001 (1000 visits/state) and so on\n"+
							"\nThe step size value deals with the updating of the state's value\n"+
				            "The formula is as follow\n"+
				            "newValue = oldValue + stepSize * ( REWARD - oldValue)","Step Size Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

        private void buttonHelpOnRewards_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("The \"win\" represents the reward or punishment you will give the agent anytime it wins\n"+
							"The \"tie\" represents the reward or punishment you will give the agent when the game is a tie\n"+
							"The \"lost\" represents the reward or punishment you will give the agent anytime it loses\n"+
							"\nPlaying around with these values can change how well and quickly the agent may learn\n"+
							"By default the agent is given a +1 for a win, a 0 for a tie, and a -1 for a lost\n"+
							"However, some other interesting rewards may be\n"+
							"win: +1, tie: -1, lost:  -1   Here anything but a win is only satisfactory\n"+
							"win: 0, tie: -1, lost: -2     Here the agent learns to fear ties and lost, and receives no positive reinforcement\n"
							,"Help on Rewards",MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

        private void buttonHelpOnAutoExplore_Click(object sender, System.EventArgs e)
		{
			MessageBox.Show("Automatic exploration of new states will allow your agent to learn\n"+
							"more quickly, by visiting states new states no matter what the other\n"+
							"values of the visited states might be. The more states that the agent\n"+
							"knows of, the smarter it will be.","Information on Automatic Exploration of New States",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

        private void buttonOK_Click(object sender, System.EventArgs e)
		{
			try
			{
				XmlTextWriter MCconfig = new XmlTextWriter("MCConfig.xml",System.Text.Encoding.Default);
				MCconfig.WriteStartDocument(true);
				MCconfig.WriteComment("This xml is used to configure the Monte Carlo learning");
				MCconfig.WriteStartElement("MonteCarlo");
				MCconfig.WriteStartElement("Epsilon");
				MCconfig.WriteString(this.numericUpDownEpsilon.Value.ToString());
				MCconfig.WriteEndElement();
				MCconfig.WriteStartElement("Stepsize");
				MCconfig.WriteString(this.numericUpDownStep.Value.ToString());
				MCconfig.WriteEndElement();
				MCconfig.WriteStartElement("Reward");
				MCconfig.WriteStartElement("Win");
				MCconfig.WriteString(this.numericUpDownWin.Value.ToString());
				MCconfig.WriteEndElement();
				MCconfig.WriteStartElement("Tie");
				MCconfig.WriteString(this.numericUpDownTie.Value.ToString());
				MCconfig.WriteEndElement();
				MCconfig.WriteStartElement("Loss");
				MCconfig.WriteString(this.numericUpDownLost.Value.ToString());
				MCconfig.WriteEndElement();
				MCconfig.WriteEndElement();
				MCconfig.WriteStartElement("AutoExplore");
				if(this.radioButtonYes.Checked)
				{
					MCconfig.WriteString("Yes");
				}
				else
				{
					MCconfig.WriteString("No");
				}
				MCconfig.WriteEndElement();
				MCconfig.WriteEndElement();
				MCconfig.WriteEndDocument();
				MCconfig.Close();
			}
			catch(Exception err)
			{
				MessageBox.Show(err.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			this.Close();
		}

	}
}
