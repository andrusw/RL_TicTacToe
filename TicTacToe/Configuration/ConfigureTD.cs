using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe.Configuration
{
	/// <summary>
	/// Summary description for ConfigureTD.
	/// </summary>
    public class ConfigureTD : ConfigForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        /// <summary>
        /// Constructor
        /// </summary>
		public ConfigureTD()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            ReadConfig("TDConfig.xml", "TemporalDifference");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureTD));
            this.buttonHelpOnStepSize = new System.Windows.Forms.Button();
            this.numericUpDownStep = new System.Windows.Forms.NumericUpDown();
            this.labelStep = new System.Windows.Forms.Label();
            this.buttonHelpOnEpsilon = new System.Windows.Forms.Button();
            this.numericUpDownEpsilon = new System.Windows.Forms.NumericUpDown();
            this.labelEpsilon = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxAutoExplore = new System.Windows.Forms.GroupBox();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.buttonHelpOnAutoExplore = new System.Windows.Forms.Button();
            this.groupBoxRewards = new System.Windows.Forms.GroupBox();
            this.labelLost = new System.Windows.Forms.Label();
            this.labelTie = new System.Windows.Forms.Label();
            this.labelWin = new System.Windows.Forms.Label();
            this.numericUpDownWin = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTie = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLost = new System.Windows.Forms.NumericUpDown();
            this.buttonHelpOnRewards = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).BeginInit();
            this.groupBoxAutoExplore.SuspendLayout();
            this.groupBoxRewards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLost)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonHelpOnStepSize
            // 
            this.buttonHelpOnStepSize.Location = new System.Drawing.Point(184, 32);
            this.buttonHelpOnStepSize.Name = "buttonHelpOnStepSize";
            this.buttonHelpOnStepSize.Size = new System.Drawing.Size(104, 20);
            this.buttonHelpOnStepSize.TabIndex = 14;
            this.buttonHelpOnStepSize.Text = "Help on Step Size";
            this.buttonHelpOnStepSize.Click += new System.EventHandler(this.buttonHelpOnStepSize_Click);
            // 
            // numericUpDownStep
            // 
            this.numericUpDownStep.DecimalPlaces = 5;
            this.numericUpDownStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownStep.Location = new System.Drawing.Point(96, 32);
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
            this.numericUpDownStep.TabIndex = 13;
            this.numericUpDownStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelStep
            // 
            this.labelStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStep.Location = new System.Drawing.Point(8, 32);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(64, 20);
            this.labelStep.TabIndex = 12;
            this.labelStep.Text = "Step Size";
            // 
            // buttonHelpOnEpsilon
            // 
            this.buttonHelpOnEpsilon.Location = new System.Drawing.Point(184, 8);
            this.buttonHelpOnEpsilon.Name = "buttonHelpOnEpsilon";
            this.buttonHelpOnEpsilon.Size = new System.Drawing.Size(104, 20);
            this.buttonHelpOnEpsilon.TabIndex = 11;
            this.buttonHelpOnEpsilon.Text = "Help on Epsilon";
            this.buttonHelpOnEpsilon.Click += new System.EventHandler(this.buttonHelpOnEpsilon_Click);
            // 
            // numericUpDownEpsilon
            // 
            this.numericUpDownEpsilon.DecimalPlaces = 4;
            this.numericUpDownEpsilon.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.numericUpDownEpsilon.Location = new System.Drawing.Point(96, 8);
            this.numericUpDownEpsilon.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownEpsilon.Name = "numericUpDownEpsilon";
            this.numericUpDownEpsilon.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownEpsilon.TabIndex = 10;
            this.numericUpDownEpsilon.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelEpsilon
            // 
            this.labelEpsilon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEpsilon.Location = new System.Drawing.Point(8, 8);
            this.labelEpsilon.Name = "labelEpsilon";
            this.labelEpsilon.Size = new System.Drawing.Size(64, 20);
            this.labelEpsilon.TabIndex = 9;
            this.labelEpsilon.Text = "Epsilon";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(232, 232);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 17;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxAutoExploreNewStates
            // 
            this.groupBoxAutoExplore.Controls.Add(this.radioButtonNo);
            this.groupBoxAutoExplore.Controls.Add(this.radioButtonYes);
            this.groupBoxAutoExplore.Controls.Add(this.buttonHelpOnAutoExplore);
            this.groupBoxAutoExplore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAutoExplore.Location = new System.Drawing.Point(0, 160);
            this.groupBoxAutoExplore.Name = "groupBoxAutoExploreNewStates";
            this.groupBoxAutoExplore.Size = new System.Drawing.Size(312, 64);
            this.groupBoxAutoExplore.TabIndex = 16;
            this.groupBoxAutoExplore.TabStop = false;
            this.groupBoxAutoExplore.Text = "Auto Explore New States?";
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
            this.groupBoxRewards.Location = new System.Drawing.Point(0, 56);
            this.groupBoxRewards.Name = "groupBoxRewards";
            this.groupBoxRewards.Size = new System.Drawing.Size(312, 98);
            this.groupBoxRewards.TabIndex = 15;
            this.groupBoxRewards.TabStop = false;
            this.groupBoxRewards.Text = "Rewards";
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
            // labelTie
            // 
            this.labelTie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTie.Location = new System.Drawing.Point(8, 48);
            this.labelTie.Name = "labelTie";
            this.labelTie.Size = new System.Drawing.Size(32, 20);
            this.labelTie.TabIndex = 2;
            this.labelTie.Text = "Tie";
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
            // ConfigureTD
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(312, 261);
            this.Controls.Add(this.buttonHelpOnStepSize);
            this.Controls.Add(this.numericUpDownStep);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.buttonHelpOnEpsilon);
            this.Controls.Add(this.numericUpDownEpsilon);
            this.Controls.Add(this.labelEpsilon);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxAutoExplore);
            this.Controls.Add(this.groupBoxRewards);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigureTD";
            this.Text = "Temporal Difference Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpsilon)).EndInit();
            this.groupBoxAutoExplore.ResumeLayout(false);
            this.groupBoxRewards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLost)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// OK Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, System.EventArgs e)
		{
            WriteConfig("TDConfig.xml", "TemporalDifference", "Temporal Difference");
			this.Close();
		}
	}
}
