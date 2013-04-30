using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace TicTacToe
{
	/// <summary>
	/// Summary description for Graph.
	/// </summary>
	public class Graph : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private double [] data = new double[8];

		public Graph()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//TODO:     READ IN DATA
			//TestData
			//data = new float[] {30.0F,40.0F,20.0F,50.0F,100.0F,1.0F,60.0F,55.0F};
			int winNum;
			int played;
			double ans;

			try
			{
				XmlTextReader win = new XmlTextReader("WinPercent.xml");
				
				win.ReadStartElement("Win");
				
				win.ReadStartElement("DPX");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[0] = ans;
				}
				else
				{
					data[0] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("DPO");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[1] = ans;
				}
				else
				{
					data[1] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();
		
				win.ReadStartElement("MCX");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[2] = ans;
				}
				else
				{
					data[2] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("MCO");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[3] = ans;
				}
				else
				{
					data[3] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("TDX");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[4] = ans;
				}
				else
				{
					data[4] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("TDO");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[5] = ans;
				}
				else
				{
					data[5] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("UserX");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[6] = ans;
				}
				else
				{
					data[6] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("UserO");
				win.ReadStartElement("Won");
				winNum = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				played = int.Parse(win.ReadString());
				if(played != 0)
				{
					ans = ((winNum * 100) / played);
					data[7] = ans;
				}
				else
				{
					data[7] = 0.0F;
				}
				win.ReadEndElement();
				win.ReadEndElement();
				win.Close();

			}
			catch(System.IO.FileNotFoundException)
			{
				for(int j = 0; j<8; j++)
				{
					data[j] = 0.0F;
				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Graph));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.GhostWhite;
			this.pictureBox1.Location = new System.Drawing.Point(0, 0);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(776, 272);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintGraph);
			// 
			// Graph
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(774, 267);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "Graph";
			this.Text = "Graph";
			this.ResumeLayout(false);

		}
		#endregion

		private void PaintGraph(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			
			Font font = new Font("Trebuchet MS",10);
			Font smallFont = new Font("Trebuchet MS",8);
			
			float barWidth = this.pictureBox1.Width/8;

            float agentBarHeight;
            PointF agentPosition;

            string[] labels = new string[] { "DP Agent X", "DP Agent O", "MC Agent X", "MC Agent O", "TD Agent X", "TD Agent O", "User X", "User O" };
            Color[] colors = new Color[] { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Indigo, Color.Violet, Color.Purple};

            for (int i = 0; i < 8; i++)
            {
                agentBarHeight = (float)data[i];
                agentPosition = new PointF(0.0F + (barWidth * i), this.pictureBox1.Height - agentBarHeight -20);

                using (Brush brush = new SolidBrush(colors[i]))
                using (Brush blackBrush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(brush, agentPosition.X, agentPosition.Y, barWidth, agentBarHeight);
                    g.DrawString(data[i].ToString() + "%", font, blackBrush, agentPosition.X + 5, agentPosition.Y - 20);
                    g.DrawString(labels[i], smallFont, blackBrush, agentPosition.X, this.pictureBox1.Height - 20);
                }
                     
            }
		}
	}
}
