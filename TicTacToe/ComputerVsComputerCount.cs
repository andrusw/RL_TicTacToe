using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe
{
	/// <summary>
	/// Summary description for ComputerVsComputerCount.
	/// </summary>
	public class ComputerVsComputerCount : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelDescription;
		private System.Windows.Forms.NumericUpDown numericUpDown;
		private System.Windows.Forms.Button buttonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public int count = 1;

		public ComputerVsComputerCount()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerVsComputerCount));
            this.labelDescription = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // labelDescription
            // 
            this.labelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDescription.Location = new System.Drawing.Point(8, 8);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(232, 40);
            this.labelDescription.TabIndex = 0;
            this.labelDescription.Text = "How many games would you want the computer to play?";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(120, 24);
            this.numericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown.TabIndex = 1;
            this.numericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(160, 56);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ComputerVsComputerCount
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(240, 85);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.labelDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ComputerVsComputerCount";
            this.Text = "ComputerVsComputerCount";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// Update XML Interplay, with count value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			count = Convert.ToInt32(this.numericUpDown.Value);
			XmlTextWriter counter = new XmlTextWriter("Interplay.xml",System.Text.Encoding.Default);
			counter.WriteStartDocument(true);
			counter.WriteComment("Just a small xml, dealing with the number of times a computer vs. computer would play");
			counter.WriteStartElement("count");
			counter.WriteString(this.numericUpDown.Value.ToString());
			counter.WriteEndElement();
			counter.WriteEndDocument();
			counter.Close();
			this.Close();
		}
	}
}
