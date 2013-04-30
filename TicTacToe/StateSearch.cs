using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe
{
	/// <summary>
	/// Summary description for StateSearch.
	/// </summary>
	public class StateSearch : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public StateSearch()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StateSearch));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(104, 16);
			this.textBox1.MaxLength = 9;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(64, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "000010000";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Enter state:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(432, 56);
			this.label2.TabIndex = 2;
			this.label2.Text = "The value 0 represents an empy spot, the value 1 represents X, and the value 2 re" +
				"presents O. The first three numbers represents the top row, the second set of th" +
				"ree numbers represent the middle row, and the last three numbers represents the " +
				"bottom row. ";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(360, 176);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "OK";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(8, 96);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 72);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Dynamic Programming";
			this.groupBox1.Visible = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 48);
			this.label3.TabIndex = 7;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Location = new System.Drawing.Point(152, 96);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(144, 72);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Monte Carlo";
			this.groupBox2.Visible = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(128, 48);
			this.label4.TabIndex = 8;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label5);
			this.groupBox3.Location = new System.Drawing.Point(296, 96);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(144, 72);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Temporal Difference";
			this.groupBox3.Visible = false;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 16);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(128, 48);
			this.label5.TabIndex = 9;
			// 
			// StateSearch
			// 
			this.AcceptButton = this.button1;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(458, 207);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "StateSearch";
			this.Text = "StateSearch";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			bool found = false;

			try
			{
					string state2 = (this.textBox1.Text[2].ToString() + this.textBox1.Text[5].ToString() + this.textBox1.Text[8].ToString() + this.textBox1.Text[1].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[7].ToString() +  this.textBox1.Text[0].ToString() +   this.textBox1.Text[3].ToString() +   this.textBox1.Text[6].ToString());
					string state3 = (this.textBox1.Text[8].ToString() + this.textBox1.Text[7].ToString() + this.textBox1.Text[6].ToString() + this.textBox1.Text[5].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[3].ToString() +  this.textBox1.Text[2].ToString() +   this.textBox1.Text[1].ToString() +   this.textBox1.Text[0].ToString());
					string state4 = (this.textBox1.Text[6].ToString() + this.textBox1.Text[3].ToString() + this.textBox1.Text[0].ToString() + this.textBox1.Text[7].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[1].ToString() +  this.textBox1.Text[8].ToString() +   this.textBox1.Text[5].ToString() +   this.textBox1.Text[2].ToString());
				
					string state5 = (this.textBox1.Text[6].ToString() + this.textBox1.Text[7].ToString() + this.textBox1.Text[8].ToString() + this.textBox1.Text[3].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[5].ToString() +  this.textBox1.Text[0].ToString() +   this.textBox1.Text[1].ToString() +   this.textBox1.Text[2].ToString());
					string state6 = (this.textBox1.Text[2].ToString() + this.textBox1.Text[1].ToString() + this.textBox1.Text[0].ToString() + this.textBox1.Text[5].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[3].ToString() +  this.textBox1.Text[8].ToString() +   this.textBox1.Text[7].ToString() +   this.textBox1.Text[6].ToString());
					string state7 = (this.textBox1.Text[8].ToString() + this.textBox1.Text[5].ToString() + this.textBox1.Text[2].ToString() + this.textBox1.Text[7].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[1].ToString() +  this.textBox1.Text[6].ToString() +   this.textBox1.Text[3].ToString() +   this.textBox1.Text[0].ToString());
					string state8 = (this.textBox1.Text[0].ToString() + this.textBox1.Text[3].ToString() + this.textBox1.Text[6].ToString() + this.textBox1.Text[1].ToString() + this.textBox1.Text[4].ToString() +  this.textBox1.Text[7].ToString() +  this.textBox1.Text[2].ToString() +   this.textBox1.Text[5].ToString() +   this.textBox1.Text[8].ToString());

				//read the state from DP
				try
				{
					XmlTextReader DPstates = new XmlTextReader("DPstates.xml");
					DPstates.ReadStartElement("DynamicProgramming");
					
					while(DPstates.Read())
					{


						if(this.textBox1.Text == DPstates.GetAttribute("string") || state2 == DPstates.GetAttribute("string") || state3 == DPstates.GetAttribute("string") || state4 == DPstates.GetAttribute("string") || state5 == DPstates.GetAttribute("string") || state6 == DPstates.GetAttribute("string")  || state7 == DPstates.GetAttribute("string") || state8 == DPstates.GetAttribute("string"))
						{
							this.label3.Text = DPstates.ReadString();
							found = true;
						}
					}
					if(found == false)
					{
						this.label3.Text = "N/A";
					}
					DPstates.Close();
				}
				catch(System.IO.FileNotFoundException)
				{
					this.label3.Text = "N/A";
				}
				catch(Exception err)
				{
					System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				}

				//Read in MC
				try
				{
					XmlTextReader MCstates = new XmlTextReader("MCstates.xml");
					MCstates.ReadStartElement("MonteCarlo");
					found = false;
					while(MCstates.Read())
					{
						if(this.textBox1.Text == MCstates.GetAttribute("string") || state2 == MCstates.GetAttribute("string") || state3 == MCstates.GetAttribute("string") || state4 == MCstates.GetAttribute("string") || state5 == MCstates.GetAttribute("string") || state6 == MCstates.GetAttribute("string")  || state7 == MCstates.GetAttribute("string") || state8 == MCstates.GetAttribute("string"))
						{
							this.label4.Text = MCstates.ReadString();
							found = true;
						}
					}
					if(found == false)
					{
						this.label4.Text = "N/A";
					}
					MCstates.Close();
				}
				catch(System.IO.FileNotFoundException)
				{
					this.label4.Text = "N/A";
				}
				catch(Exception err)
				{
					System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				}


				found = false;
				try
				{
					XmlTextReader TDstates = new XmlTextReader("TDstates.xml");
					TDstates.ReadStartElement("TemporalDifference");
					
					while(TDstates.Read())
					{
						if(this.textBox1.Text == TDstates.GetAttribute("string") || state2 == TDstates.GetAttribute("string") || state3 == TDstates.GetAttribute("string") || state4 == TDstates.GetAttribute("string") || state5 == TDstates.GetAttribute("string") || state6 == TDstates.GetAttribute("string")  || state7 == TDstates.GetAttribute("string") || state8 == TDstates.GetAttribute("string"))
						{
							this.label5.Text = TDstates.ReadString();
							found = true;
						}
					}
					if(found == false)
					{
						this.label5.Text = "N/A";
					}
					TDstates.Close();
				}
				catch(System.IO.FileNotFoundException)
				{
					this.label5.Text = "N/A";
				}
				catch(Exception err)
				{
					System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				}

				this.groupBox1.Visible = true;
				this.groupBox2.Visible = true;
				this.groupBox3.Visible = true;
			}
			catch(System.IndexOutOfRangeException)
			{
				this.label3.Text = "N/A";
				this.label4.Text = "N/A";
				this.label5.Text = "N/A";
			}
		}
	}
}
