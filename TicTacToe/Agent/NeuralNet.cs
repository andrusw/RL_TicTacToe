using System;
using System.Xml;

namespace TicTacToe.Agent
{
	/// <summary>
	/// Summary description for NeuralNet.
	/// </summary>
	public class NeuralNet
	{
		private int player;

		public NeuralNet(int player)
		{
			this.player = player;
			double StepSize = 0.1;
			int winReward = 1;
			int tieReward = 0;
			int lostReward = -1;
			const int MAX_TIME = 9;
			const int MAX_FIRSTLEVEL = 18;
			double [,] Level1 = new double[MAX_TIME,MAX_FIRSTLEVEL];
			const int MAX_SECONDLEVEL = 24;
			double [,] Level2 = new double[MAX_TIME,MAX_SECONDLEVEL];
			const int MAX_THIRDLEVEL = 16;
			double [,] Level3 = new double[MAX_TIME,MAX_THIRDLEVEL];
			/*
			 * Read in the configuration
			 * 
			 */ 
			try
			{
				XmlTextReader Neuralconfig = new XmlTextReader("NeuralConfig.xml");
				
				Neuralconfig.ReadStartElement("Neural");
				Neuralconfig.ReadStartElement("Stepsize");
				StepSize = double.Parse(Neuralconfig.ReadString());
				Neuralconfig.ReadEndElement();
				Neuralconfig.ReadStartElement("Reward");
				Neuralconfig.ReadStartElement("Win");
				winReward = int.Parse(Neuralconfig.ReadString());
				Neuralconfig.ReadEndElement();
				Neuralconfig.ReadStartElement("Tie");
				tieReward = int.Parse(Neuralconfig.ReadString());
				Neuralconfig.ReadEndElement();
				Neuralconfig.ReadStartElement("Loss");
				lostReward = int.Parse(Neuralconfig.ReadString());
				Neuralconfig.ReadEndElement();
				Neuralconfig.ReadEndElement();
				Neuralconfig.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}

			/*
			 * Read in the weights
			 * 
			 * <?xml>
			 * <NeuralNetwork>
			 * <weights time=0>
			 * <weight level=1.0>
			 * .5
			 * </weight>
			 * ...
			 * <weight level=2.16>
			 * -.5
			 * </weight>
			 * </weights>
			 * ...
			 * <weights time=8>
			 * </weights>
			 * </xml>
			 */ 
			try
			{
				XmlTextReader NeuralWeights = new XmlTextReader("NeuralWeights.xml");
				NeuralWeights.ReadStartElement("NeuralNetwork");
				
				NeuralWeights.ReadStartElement();
				for(int j = 0; j < MAX_TIME; j++)
				{
					NeuralWeights.ReadStartElement();
					for(int i = 0; i < MAX_FIRSTLEVEL; i++)
					{
						NeuralWeights.ReadString();

					}

					for(int i = 0; i < MAX_SECONDLEVEL; i++)
					{
					}

					for(int i = 0; i< MAX_THIRDLEVEL; i++)
					{
					}
					NeuralWeights.ReadEndElement();
				}
				NeuralWeights.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//write new file and store it

			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
	}
}
