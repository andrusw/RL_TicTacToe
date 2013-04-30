using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace TicTacToe
{
	/// <summary>
	/// Summary description for OpenXML.
	/// </summary>
	public class OpenXML : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TreeView treeView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public OpenXML(string xmlFile)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			XmlTextReader xml = new XmlTextReader(xmlFile);
			xml.MoveToContent();
			this.treeView1.Nodes.Add("Data");
			this.treeView1.Nodes[0].Nodes.Add("states");
			int i = 0;
			while(xml.Read())
			{
				this.treeView1.Nodes[0].Nodes[0].Nodes.Add(xml.GetAttribute("string"));
				this.treeView1.Nodes[0].Nodes[0].Nodes[i].Nodes.Add("value");
				this.treeView1.Nodes[0].Nodes[0].Nodes[i].Nodes[0].Nodes.Add(xml.ReadString());
				i++;
			}

			xml.Close();
			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OpenXML));
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(0, 0);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(488, 480);
			this.treeView1.TabIndex = 0;
			// 
			// OpenXML
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 477);
			this.Controls.Add(this.treeView1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OpenXML";
			this.Text = "View XML";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
