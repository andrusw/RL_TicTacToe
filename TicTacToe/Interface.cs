using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Xml;
using System.IO;
using TicTacToe.Game;
using TicTacToe.Configuration;

namespace TicTacToe
{

	public class FormTicTacToe : System.Windows.Forms.Form
    {
        #region Interface Setup
        private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemFileNew;
		private System.Windows.Forms.MenuItem menuItemFileNewGame;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSinglePlayer;
		private System.Windows.Forms.MenuItem menuItemFileNewGameTwoPlayer;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSinglePlayerAsX;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSinglePlayerAsO;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSimulation;
		private System.Windows.Forms.MenuItem menuItemFileClose;
		private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.MenuItem menuItemEdit;
		private System.Windows.Forms.MenuItem menuItemEditAgent;
		private System.Windows.Forms.MenuItem menuItemEditAgentLearningStyleDP;
		private System.Windows.Forms.MenuItem menuItemEditAgentLearningStyleMC;
		private System.Windows.Forms.MenuItem menuItemEditAgentLearningStyleTD;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemHelpHowToPlay;
		private System.Windows.Forms.MenuItem menuItemHelpAbout;
		private System.Windows.Forms.MenuItem menuItemEditAgentLearningStyle;
		private System.Windows.Forms.MenuItem menuItemEditOpponent;
		private System.Windows.Forms.MenuItem menuItemEditOpponentDPAgent;
		private System.Windows.Forms.MenuItem menuItemEditOpponentMCAgent;
		private System.Windows.Forms.MenuItem menuItemEditOpponentTDAgent;
		private System.Windows.Forms.MenuItem menuItemData;
		private System.Windows.Forms.MenuItem menuItemDataXML;
		private System.Windows.Forms.MenuItem menuItemDataXMLDP;
		private System.Windows.Forms.MenuItem menuItemDataXMLMC;
		private System.Windows.Forms.MenuItem menuItemDataXMLTD;
		private System.Windows.Forms.MenuItem menuItemDataState;
		private System.Windows.Forms.MenuItem menuItemDataWinPercent;
		private System.Windows.Forms.MenuItem menuItemHelpReinforcementLearingInfo;
		private System.Windows.Forms.MenuItem menuItemEditWatchCount;		
        private System.Windows.Forms.StatusBarPanel statusBarPanel;
		private System.Windows.Forms.MenuItem menuItemDataTotalStates;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSimulationAgainstDP;
		private System.Windows.Forms.MenuItem menuItemFileNewGameSimulationAgainstMC; 
        private System.Windows.Forms.MenuItem menuItemFileNewGameSimulationAgainstTD;
        private IContainer components;
        #endregion

        /*
         * Calculate values for faster retrieval and drawing per state
         */
        private float ThirdWidth { get; set; }
        private float ThirdHeight { get; set; }
        private float TwoThirdWidth { get; set; }
        private float TwoThirdHeight { get; set; }

        //Used to find where user has clicked
        private int[,] arrayPositionReference = new int[3, 3] { { 0, 3, 6 }, { 1, 4, 7 }, { 2, 5, 8 } };

        protected const int BOARD_SIZE = 9;
        protected int turn = 0;
		protected int[] currentState = new int[BOARD_SIZE]{0,0,0,0,0,0,0,0,0}; 
		protected int[] previousState = new int[BOARD_SIZE]{0,0,0,0,0,0,0,0,0};
		protected int win = 0;
		
        private GameType gameType = 0;

        private int movesLeft = BOARD_SIZE;
		private Agent.DynamicProgramming agentDPO;
		private Agent.DynamicProgramming agentDPX;
		private Agent.MonteCarlo agentMCO;
		private Agent.MonteCarlo agentMCX;
		private Agent.TemporalDifference agentTDO;
		private Agent.TemporalDifference agentTDX;
		private int winner = -1;
		private int count = 1;

        public delegate int GetMove(int[] currentState);
        public delegate void Update(GameResult result, int[] board, GameType gametype);
        

		public FormTicTacToe()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ReadCount();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTicTacToe));
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemFileNew = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGame = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSinglePlayer = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSinglePlayerAsX = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSinglePlayerAsO = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameTwoPlayer = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSimulation = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSimulationAgainstDP = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSimulationAgainstMC = new System.Windows.Forms.MenuItem();
            this.menuItemFileNewGameSimulationAgainstTD = new System.Windows.Forms.MenuItem();
            this.menuItemFileClose = new System.Windows.Forms.MenuItem();
            this.menuItemEdit = new System.Windows.Forms.MenuItem();
            this.menuItemEditAgent = new System.Windows.Forms.MenuItem();
            this.menuItemEditAgentLearningStyle = new System.Windows.Forms.MenuItem();
            this.menuItemEditAgentLearningStyleDP = new System.Windows.Forms.MenuItem();
            this.menuItemEditAgentLearningStyleMC = new System.Windows.Forms.MenuItem();
            this.menuItemEditAgentLearningStyleTD = new System.Windows.Forms.MenuItem();
            this.menuItemEditOpponent = new System.Windows.Forms.MenuItem();
            this.menuItemEditOpponentDPAgent = new System.Windows.Forms.MenuItem();
            this.menuItemEditOpponentMCAgent = new System.Windows.Forms.MenuItem();
            this.menuItemEditOpponentTDAgent = new System.Windows.Forms.MenuItem();
            this.menuItemEditWatchCount = new System.Windows.Forms.MenuItem();
            this.menuItemData = new System.Windows.Forms.MenuItem();
            this.menuItemDataXML = new System.Windows.Forms.MenuItem();
            this.menuItemDataXMLDP = new System.Windows.Forms.MenuItem();
            this.menuItemDataXMLMC = new System.Windows.Forms.MenuItem();
            this.menuItemDataXMLTD = new System.Windows.Forms.MenuItem();
            this.menuItemDataState = new System.Windows.Forms.MenuItem();
            this.menuItemDataWinPercent = new System.Windows.Forms.MenuItem();
            this.menuItemDataTotalStates = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemHelpHowToPlay = new System.Windows.Forms.MenuItem();
            this.menuItemHelpReinforcementLearingInfo = new System.Windows.Forms.MenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.MenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 285);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(352, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "Ready";
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.Name = "statusBarPanel1";
            this.statusBarPanel.Text = "Ready";
            this.statusBarPanel.Width = 140;
            // 
            // progressBar
            // 
            this.progressBar.Enabled = false;
            this.progressBar.Location = new System.Drawing.Point(144, 287);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(200, 20);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemEdit,
            this.menuItemData,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileNew,
            this.menuItemFileClose});
            this.menuItemFile.Text = "File";
            // 
            // menuItemFileNew
            // 
            this.menuItemFileNew.Index = 0;
            this.menuItemFileNew.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileNewGame});
            this.menuItemFileNew.Text = "&New";
            // 
            // menuItemFileNewGame
            // 
            this.menuItemFileNewGame.Index = 0;
            this.menuItemFileNewGame.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileNewGameSinglePlayer,
            this.menuItemFileNewGameTwoPlayer,
            this.menuItemFileNewGameSimulation});
            this.menuItemFileNewGame.Text = "Game";
            // 
            // menuItemFileNewGameSinglePlayer
            // 
            this.menuItemFileNewGameSinglePlayer.Index = 0;
            this.menuItemFileNewGameSinglePlayer.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileNewGameSinglePlayerAsX,
            this.menuItemFileNewGameSinglePlayerAsO});
            this.menuItemFileNewGameSinglePlayer.Text = "Single Player";
            // 
            // menuItemFileNewGameSinglePlayerAsX
            // 
            this.menuItemFileNewGameSinglePlayerAsX.Index = 0;
            this.menuItemFileNewGameSinglePlayerAsX.Text = "You as X";
            this.menuItemFileNewGameSinglePlayerAsX.Click += new System.EventHandler(this.menuItemFileNewGameSinglePlayerAsX_Click);
            // 
            // menuItemFileNewGameSinglePlayerAsO
            // 
            this.menuItemFileNewGameSinglePlayerAsO.Index = 1;
            this.menuItemFileNewGameSinglePlayerAsO.Text = "You as O";
            this.menuItemFileNewGameSinglePlayerAsO.Click += new System.EventHandler(this.menuItemFileNewGameSinglePlayerAsO_Click);
            // 
            // menuItemFileNewGameTwoPlayer
            // 
            this.menuItemFileNewGameTwoPlayer.Index = 1;
            this.menuItemFileNewGameTwoPlayer.Text = "Two Player";
            this.menuItemFileNewGameTwoPlayer.Click += new System.EventHandler(this.menuItemFileNewGameTwoPlayer_Click);
            // 
            // menuItemFileNewGameSimulation
            // 
            this.menuItemFileNewGameSimulation.Index = 2;
            this.menuItemFileNewGameSimulation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileNewGameSimulationAgainstDP,
            this.menuItemFileNewGameSimulationAgainstMC,
            this.menuItemFileNewGameSimulationAgainstTD});
            this.menuItemFileNewGameSimulation.Text = "Simulation";
            this.menuItemFileNewGameSimulation.Click += new System.EventHandler(this.menuItemFileNewGameSimulation_Click);
            // 
            // menuItemFileNewGameSimulationAgainstDP
            // 
            this.menuItemFileNewGameSimulationAgainstDP.Index = 0;
            this.menuItemFileNewGameSimulationAgainstDP.Text = "Against Dynamical Programming";
            this.menuItemFileNewGameSimulationAgainstDP.Click += new System.EventHandler(this.menuItemFileNewGameSimulationAgainstDP_Click);
            // 
            // menuItemFileNewGameSimulationAgainstMC
            // 
            this.menuItemFileNewGameSimulationAgainstMC.Checked = true;
            this.menuItemFileNewGameSimulationAgainstMC.Index = 1;
            this.menuItemFileNewGameSimulationAgainstMC.Text = "Against Monte Carlo";
            this.menuItemFileNewGameSimulationAgainstMC.Click += new System.EventHandler(this.menuItemFileNewGameSimulationAgainstMC_Click);
            // 
            // menuItemFileNewGameSimulationAgainstTD
            // 
            this.menuItemFileNewGameSimulationAgainstTD.Index = 2;
            this.menuItemFileNewGameSimulationAgainstTD.Text = "Against Temporal Difference";
            this.menuItemFileNewGameSimulationAgainstTD.Click += new System.EventHandler(this.menuItemFileNewGameSimulationAgainstTD_Click);
            // 
            // menuItemFileClose
            // 
            this.menuItemFileClose.Index = 1;
            this.menuItemFileClose.Text = "&Close";
            this.menuItemFileClose.Click += new System.EventHandler(this.menuItemFileClose_Click);
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 1;
            this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditAgent,
            this.menuItemEditOpponent,
            this.menuItemEditWatchCount});
            this.menuItemEdit.Text = "Edit";
            // 
            // menuItemEditAgent
            // 
            this.menuItemEditAgent.Index = 0;
            this.menuItemEditAgent.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditAgentLearningStyle});
            this.menuItemEditAgent.Text = "Agent";
            // 
            // menuItemEditAgentLearningStyle
            // 
            this.menuItemEditAgentLearningStyle.Index = 0;
            this.menuItemEditAgentLearningStyle.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditAgentLearningStyleDP,
            this.menuItemEditAgentLearningStyleMC,
            this.menuItemEditAgentLearningStyleTD});
            this.menuItemEditAgentLearningStyle.Text = "Learning Style";
            // 
            // menuItemEditAgentLearningStyleDP
            // 
            this.menuItemEditAgentLearningStyleDP.Index = 0;
            this.menuItemEditAgentLearningStyleDP.Text = "Dynamic Programming";
            this.menuItemEditAgentLearningStyleDP.Click += new System.EventHandler(this.menuItemEditAgentLearningStyleDP_Click);
            // 
            // menuItemEditAgentLearningStyleMC
            // 
            this.menuItemEditAgentLearningStyleMC.Index = 1;
            this.menuItemEditAgentLearningStyleMC.Text = "Monte Carlo";
            this.menuItemEditAgentLearningStyleMC.Click += new System.EventHandler(this.menuItemEditAgentLearningStyleMC_Click);
            // 
            // menuItemEditAgentLearningStyleTD
            // 
            this.menuItemEditAgentLearningStyleTD.Index = 2;
            this.menuItemEditAgentLearningStyleTD.Text = "Temporal Difference";
            this.menuItemEditAgentLearningStyleTD.Click += new System.EventHandler(this.menuItemEditAgentLearningStyleTD_Click);
            // 
            // menuItemEditOpponent
            // 
            this.menuItemEditOpponent.Index = 1;
            this.menuItemEditOpponent.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditOpponentDPAgent,
            this.menuItemEditOpponentMCAgent,
            this.menuItemEditOpponentTDAgent});
            this.menuItemEditOpponent.Text = "Opponent";
            // 
            // menuItemEditOpponentDPAgent
            // 
            this.menuItemEditOpponentDPAgent.Index = 0;
            this.menuItemEditOpponentDPAgent.Text = "Dynamic Programming Agent";
            this.menuItemEditOpponentDPAgent.Click += new System.EventHandler(this.menuItemEditOpponentDPAgent_Click);
            // 
            // menuItemEditOpponentMCAgent
            // 
            this.menuItemEditOpponentMCAgent.Checked = true;
            this.menuItemEditOpponentMCAgent.DefaultItem = true;
            this.menuItemEditOpponentMCAgent.Index = 1;
            this.menuItemEditOpponentMCAgent.Text = "Monte Carlo Agent";
            this.menuItemEditOpponentMCAgent.Click += new System.EventHandler(this.menuItemEditOpponentMCAgent_Click);
            // 
            // menuItemEditOpponentTDAgent
            // 
            this.menuItemEditOpponentTDAgent.Index = 2;
            this.menuItemEditOpponentTDAgent.Text = "Temporal Difference Agent";
            this.menuItemEditOpponentTDAgent.Click += new System.EventHandler(this.menuItemEditOpponentTDAgent_Click);
            // 
            // menuItemEditWatchCount
            // 
            this.menuItemEditWatchCount.Index = 2;
            this.menuItemEditWatchCount.Text = "Watch Count";
            this.menuItemEditWatchCount.Click += new System.EventHandler(this.menuItemEditWatchCount_Click);
            // 
            // menuItemData
            // 
            this.menuItemData.Index = 2;
            this.menuItemData.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDataXML,
            this.menuItemDataState,
            this.menuItemDataWinPercent,
            this.menuItemDataTotalStates});
            this.menuItemData.Text = "Data";
            // 
            // menuItemDataXML
            // 
            this.menuItemDataXML.Index = 0;
            this.menuItemDataXML.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemDataXMLDP,
            this.menuItemDataXMLMC,
            this.menuItemDataXMLTD});
            this.menuItemDataXML.Text = "XML";
            // 
            // menuItemDataXMLDP
            // 
            this.menuItemDataXMLDP.Index = 0;
            this.menuItemDataXMLDP.Text = "Dynamic Programming";
            this.menuItemDataXMLDP.Click += new System.EventHandler(this.menuItemDataXMLDP_Click);
            // 
            // menuItemDataXMLMC
            // 
            this.menuItemDataXMLMC.Index = 1;
            this.menuItemDataXMLMC.Text = "Monte Carlo";
            this.menuItemDataXMLMC.Click += new System.EventHandler(this.menuItemDataXMLMC_Click);
            // 
            // menuItemDataXMLTD
            // 
            this.menuItemDataXMLTD.Index = 2;
            this.menuItemDataXMLTD.Text = "Temporal Difference";
            this.menuItemDataXMLTD.Click += new System.EventHandler(this.menuItemDataXMLTD_Click);
            // 
            // menuItemDataState
            // 
            this.menuItemDataState.Index = 1;
            this.menuItemDataState.Text = "State";
            this.menuItemDataState.Click += new System.EventHandler(this.menuItemDataState_Click);
            // 
            // menuItemDataWinPercent
            // 
            this.menuItemDataWinPercent.Index = 2;
            this.menuItemDataWinPercent.Text = "Win Percentage";
            this.menuItemDataWinPercent.Click += new System.EventHandler(this.menuItemDataWinPercentage_Click);
            // 
            // menuItemDataTotalStates
            // 
            this.menuItemDataTotalStates.Index = 3;
            this.menuItemDataTotalStates.Text = "Total States";
            this.menuItemDataTotalStates.Click += new System.EventHandler(this.menuItemDataTotalStates_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 3;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemHelpHowToPlay,
            this.menuItemHelpReinforcementLearingInfo,
            this.menuItemHelpAbout});
            this.menuItemHelp.Text = "Help";
            // 
            // menuItemHelpHowToPlay
            // 
            this.menuItemHelpHowToPlay.Index = 0;
            this.menuItemHelpHowToPlay.Text = "How To Play";
            this.menuItemHelpHowToPlay.Click += new System.EventHandler(this.menuItemHelpHowToPlay_Click);
            // 
            // menuItemHelpReinforcementLearingInfo
            // 
            this.menuItemHelpReinforcementLearingInfo.Index = 1;
            this.menuItemHelpReinforcementLearingInfo.Text = "Reinforcement Learning Info";
            this.menuItemHelpReinforcementLearingInfo.Click += new System.EventHandler(this.menuItemHelpReinforcementLearingInfo_Click);
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Index = 2;
            this.menuItemHelpAbout.Text = "&About";
            this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(352, 285);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawGameBoard);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            // 
            // FormTicTacToe
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(352, 307);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.statusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "FormTicTacToe";
            this.Text = "Tic Tac Toe";
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
            Application.Run(new FormTicTacToe());
		}

        #region File

        /// <summary>
        /// Close Program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
        
        /// <summary>
        /// Play new game - User as X
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemFileNewGameSinglePlayerAsX_Click(object sender, System.EventArgs e)
		{
			this.turn = 0;
            movesLeft = BOARD_SIZE;
			win = 0;
			gameType = GameType.UserXVsAgent;
            currentState = new int[BOARD_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			//User.User userX = new User.User();

            //Player makes first move, so just need to initialize agents
			if(this.menuItemEditOpponentDPAgent.Checked)
			{
				agentDPO = new Agent.DynamicProgramming(Player.Second);
			}
			else if(this.menuItemEditOpponentMCAgent.Checked)
			{
				agentMCO = new Agent.MonteCarlo(Player.Second);
			}
			else //this menuItem22.Checked
			{
				agentTDO = new Agent.TemporalDifference(Player.Second);
			}

			this.pictureBox.Invalidate();
		}        
        
        /// <summary>
        /// Play new game - User as O
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemFileNewGameSinglePlayerAsO_Click(object sender, System.EventArgs e)
		{
			this.turn = 0;
            movesLeft = BOARD_SIZE;
			win = 0;
			gameType = GameType.UserOVsAgent;
            currentState = new int[BOARD_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            GetMove getMove;

            //Agent makes first, need to pull first move.
			if(this.menuItemEditOpponentDPAgent.Checked)
			{
				agentDPX = new Agent.DynamicProgramming(Player.First);
                getMove = agentDPX.getMove;
			}
			else if(this.menuItemEditOpponentMCAgent.Checked)
			{
                agentMCX = new Agent.MonteCarlo(Player.First);
                getMove = agentMCX.getMove;
			}
			else //this menuItem22.Checked
			{
                agentTDX = new Agent.TemporalDifference(Player.First);
                getMove = agentTDX.getMove;
			}
            currentState[getMove(currentState)] = 1;
            turn = 1;
			//User.User userO = new User.User();
			this.pictureBox.Invalidate();
		}
        		
        /// <summary>
        /// Play new game - User vs. User
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemFileNewGameTwoPlayer_Click(object sender, System.EventArgs e)
		{
			this.turn = 0;
            movesLeft = BOARD_SIZE;
			win = 0;
			gameType = GameType.UserVsUser;
            currentState = new int[BOARD_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
			User.User userO = new User.User();
			User.User userX = new User.User();
			this.pictureBox.Invalidate();
		}

        /// <summary>
        /// Play new game - Agent vs. Agent Simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileNewGameSimulation_Click(object sender, System.EventArgs e)
        {
            ReadCount();

            this.progressBar.Maximum = count;
            this.progressBar.Step = 1;
            this.progressBar.Show();
            this.progressBar.Value = 0;

            this.statusBarPanel.Text = "Running Simulations";
            this.statusBar.Invalidate();

            GetMove getMoveX;
            GetMove getMoveO;
            Update updateX;
            Update updateO;

            #region Agent Declarations
            if (this.menuItemEditOpponentDPAgent.Checked)
            {
                agentDPX = new Agent.DynamicProgramming(Player.First);
                getMoveX = agentDPX.getMove;
                updateX = agentDPX.update;
            }
            else if (this.menuItemEditOpponentMCAgent.Checked)
            {
                agentMCX = new Agent.MonteCarlo(Player.First);
                getMoveX = agentMCX.getMove;
                updateX = agentMCX.update;
            }
            else// (this.menuItemEditOpponentTDAgent.Checked)
            {
                agentTDX = new Agent.TemporalDifference(Player.First);
                getMoveX = agentTDX.getMove;
                updateX = agentTDX.update;
            }

            if (this.menuItemFileNewGameSimulationAgainstMC.Checked)
            {
                agentMCO = new Agent.MonteCarlo(Player.Second);
                getMoveO = agentMCO.getMove;
                updateO = agentMCO.update;
            }
            else if (this.menuItemFileNewGameSimulationAgainstTD.Checked)
            {
                agentTDO = new Agent.TemporalDifference(Player.Second);
                getMoveO = agentTDO.getMove;
                updateO = agentTDO.update;
            }
            else//(this.menuItem33.Checked || default)
            {
                agentDPO = new Agent.DynamicProgramming(Player.Second);
                getMoveO = agentDPO.getMove;
                updateO = agentDPO.update;
            }
            #endregion

            for (int j = 0; j < count; j++)
            {
                if (agentMCO != null)
                    agentMCO.Reset();

                if (agentMCX != null)
                    agentMCX.Reset();

                this.progressBar.Increment(this.progressBar.Step);
                this.pictureBox.Invalidate();

                movesLeft = BOARD_SIZE;
                win = 0;
                gameType = GameType.AgentVsAgent;
                currentState = new int[BOARD_SIZE] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                bool turnBase = true;

                #region Play until win
                while (this.win == 0)
                {
                    if (turnBase)
                    {
                        turnBase = false;
                        MakeAgentVsAgentMove(getMoveX, Player.First);
                    }
                    else
                    {
                        turnBase = true;
                        MakeAgentVsAgentMove(getMoveO,Player.Second);
                    }
                }
                #endregion

                #region update results to agent
                if (winner == 1)//X won
                {
                    updateX(GameResult.Win, currentState, gameType);
                    updateO(GameResult.Lost, previousState, gameType);
                }
                else if (winner == 2)//O won
                {
                    updateX(GameResult.Lost, previousState, gameType);
                    updateO(GameResult.Win, currentState, gameType);
                }
                else //Cat's game
                {
                    if (turn == 0)
                    {
                        updateX(GameResult.Tie, previousState, gameType);
                        updateO(GameResult.Tie, currentState, gameType);
                    }
                    else //turn ==1
                    {
                        updateX(GameResult.Tie, currentState, gameType);
                        updateO(GameResult.Tie, currentState, gameType);
                    }
                }
                #endregion

                this.pictureBox.Invalidate();
            }

            this.statusBarPanel.Text = "Done";
            this.progressBar.Hide();
            this.statusBar.Invalidate();
        }

        /// <summary>
        /// Make the Agent vs. Agent move, updates and checks.
        /// </summary>
        /// <param name="getMove"></param>
        /// <param name="player"></param>
        private void MakeAgentVsAgentMove(GetMove getMove, Player player)
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                previousState[i] = currentState[i];
            }
            currentState[getMove(currentState)] = (int)player;
            turn = (turn + 1) % 2;
            checkWin();
            this.movesLeft--;
            this.pictureBox.Invalidate();
        }

        /// <summary>
        /// Select Who the agent play against - Dynamic Programming
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileNewGameSimulationAgainstDP_Click(object sender, System.EventArgs e)
        {
            this.menuItemFileNewGameSimulationAgainstDP.Checked = true;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = false;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = false;
            menuItemFileNewGameSimulation_Click(sender, e);
        }

        /// <summary>
        /// Select Who the agent play against - Monte Carlo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileNewGameSimulationAgainstMC_Click(object sender, System.EventArgs e)
        {
            this.menuItemFileNewGameSimulationAgainstDP.Checked = false;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = true;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = false;
            menuItemFileNewGameSimulation_Click(sender, e);
        }

        /// <summary>
        /// Select Who the agent play against - Temporal Difference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemFileNewGameSimulationAgainstTD_Click(object sender, System.EventArgs e)
        {
            this.menuItemFileNewGameSimulationAgainstDP.Checked = false;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = false;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = true;
            menuItemFileNewGameSimulation_Click(sender, e);
        }	

        #endregion

        #region Edit
        /// <summary>
        /// Edit Agent Learning Style - Agent: Dynamic Programming
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemEditAgentLearningStyleDP_Click(object sender, System.EventArgs e)
		{
			Form configDP = new Configuration.ConfigureDP();
			configDP.Show();
		}

        /// <summary>
        /// Edit Agent Learning Style - Agent: Monte Carlo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditAgentLearningStyleMC_Click(object sender, System.EventArgs e)
		{
            Form configMC = new Configuration.ConfigureMC();
			configMC.Show();
		}

        /// <summary>
        /// Edit Agent Learning Style - Temporal Difference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditAgentLearningStyleTD_Click(object sender, System.EventArgs e)
		{
            Form configTD = new Configuration.ConfigureTD();
			configTD.Show();
		}

        /// <summary>
        /// Change Opponent(Agent) to Dynamic Programming
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditOpponentDPAgent_Click(object sender, System.EventArgs e)
        {

            this.menuItemEditOpponentMCAgent.Checked = false;
            this.menuItemEditOpponentDPAgent.Checked = true;
            this.menuItemEditOpponentTDAgent.Checked = false;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = false;
            this.menuItemFileNewGameSimulationAgainstDP.Checked = true;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = false;
        }

        /// <summary>
        /// Change Opponent(Agent) to Monte Carlo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditOpponentMCAgent_Click(object sender, System.EventArgs e)
        {

            this.menuItemEditOpponentMCAgent.Checked = true;
            this.menuItemEditOpponentDPAgent.Checked = false;
            this.menuItemEditOpponentTDAgent.Checked = false;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = true;
            this.menuItemFileNewGameSimulationAgainstDP.Checked = false;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = false;
        }

        /// <summary>
        /// Change Opponent(Agent) to Temporal Difference
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditOpponentTDAgent_Click(object sender, System.EventArgs e)
        {

            this.menuItemEditOpponentMCAgent.Checked = false;
            this.menuItemEditOpponentDPAgent.Checked = false;
            this.menuItemEditOpponentTDAgent.Checked = true;
            this.menuItemFileNewGameSimulationAgainstMC.Checked = false;
            this.menuItemFileNewGameSimulationAgainstDP.Checked = false;
            this.menuItemFileNewGameSimulationAgainstTD.Checked = true;
        }

        /// <summary>
        /// Edit - How many games will agent vs. agent play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemEditWatchCount_Click(object sender, System.EventArgs e)
        {
            TicTacToe.ComputerVsComputerCount countForm = new ComputerVsComputerCount();
            countForm.Show();
        }

        #endregion

        #region Help

        /// <summary>
        /// Help - About
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHelpAbout_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Originally Developed by William Andrus in 2004 & 2006\n", "About Reinforcement Learning Tic-Tac-Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Help - How to play Tic-Tac-Toe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHelpHowToPlay_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show("Tic-Tac-Toe (Also known as: Naught and Crosses)\n\n" +
                            "Tic-Tac-Toe is a two-player game where X represents one player,\n" +
                            "and O represents the other player. The game is played on a 3x3 grid,\n" +
                            "and the first player to get 3 Xs or Os in a row wins. This can\n" +
                            "be done horizontally, vertically, or diagonally. In the event of\n" +
                            "no more moves are left and neither player wins is called a draw (AKA: Cat's Game).", "How to play tic-tac-toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Help - About Reinforcement Learning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemHelpReinforcementLearingInfo_Click(object sender, System.EventArgs e)
        {
            MessageBox.Show(@"
INTRODUCTION:
Artificial Intelligence (AI) has grown into a large area of research for developing concepts 
of creating human-like intelligence. One area of application is the development of programs 
to play games at a competitive level. One promising AI technique is Reinforcement Learning (RL). 
These concepts allow for a better understanding of AI itself by refining and applying these 
ideas to a wide spectrum of problem areas of varying difficulty while maintaining a simple 
understanding of what is being accomplished.

REINFORCEMENT LEARNING:
Reinforcement learning is the process for a program to interact within an environment and 
learn by receiving numerical rewards or punishment while trying to obtain a goal. In this case, 
the goal is to win when dealing with tic-tac-toe. The environment is “where” and “how” a program, 
that is using reinforcement learning, interacts against its opponents in the game. The reward/punishment 
is a scalar value. Depending on the program’s decisions and actions, it will make a move and recieve an
reward later.

DYNAMIC PROGRAMMING:
In dynamic programming the reward for each move is based upon all the possible values that could have
been taken. This concept is called a full backup, where each move is given a reward after is has been
taken.  

MONTE CARLO METHOD:
In monte carlo the reward for each of the moves made in the game is based upon the ending result, where
each of the moves would be given a reward, when the game has finished.

TEMPORAL DIFFERENCE:
In temporal difference the reward is given after each move (like Dynamic Programming), but the reward is
based on the move that was made (like monte carlo method).", "Small Introduction to Reinforcement Learning", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Data

        /// <summary>
        /// Show Win Percentage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemDataWinPercentage_Click(object sender, System.EventArgs e)
		{
			Graph graph = new Graph();
			graph.Show();
		}

        /// <summary>
        /// Show Dynamic Programming XML States
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemDataXMLDP_Click(object sender, System.EventArgs e)
		{
			OpenXML file = new OpenXML("DPstates.xml");
			file.Show();
		}

        /// <summary>
        /// Show Monte Carlo XML States
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemDataXMLMC_Click(object sender, System.EventArgs e)
		{
			OpenXML file = new OpenXML("MCstates.xml");
			file.Show();

		}

        /// <summary>
        /// Show Temporal Difference XML States
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemDataXMLTD_Click(object sender, System.EventArgs e)
        {
            OpenXML file = new OpenXML("TDstates.xml");
            file.Show();
        }

        /// <summary>
        /// Search XML for specific state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemDataState_Click(object sender, System.EventArgs e)
		{
			StateSearch statesearch = new StateSearch();
			statesearch.Show();
		}

        /// <summary>
        /// A count of total states seen for each agent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void menuItemDataTotalStates_Click(object sender, System.EventArgs e)
		{
			int DPcount = 0;
			int MCcount = 0;
			int TDcount = 0;
			//int MAX_COUNT = 6620; //possible tictactoe states given this games layout

			//Count DP states
			try
			{
				XmlTextReader DPstates = new XmlTextReader("DPstates.xml");
				DPstates.ReadStartElement("DynamicProgramming");
				while(DPstates.Read())
				{
					if(DPstates.HasAttributes)
					{
						DPcount++;
						DPstates.ReadString();
					}
				}
				DPstates.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}

			//Count MC states
			try
			{
				XmlTextReader MCstates = new XmlTextReader("MCstates.xml");
				MCstates.ReadStartElement("MonteCarlo");
				while(MCstates.Read())
				{
					if(MCstates.HasAttributes)
					{
						MCcount++;
						MCstates.ReadString();
					}
				}
				MCstates.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}

			//Count TD states
			try
			{
				XmlTextReader TDstates = new XmlTextReader("TDstates.xml");
				TDstates.ReadStartElement("TemporalDifference");
				while(TDstates.Read())
				{
					if(TDstates.HasAttributes)
					{
						TDstates.ReadString();
						TDcount++;
					}
				}
				TDstates.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				//do nothing, use defaults
			}
			catch(Exception err)
			{
				
				System.Windows.Forms.MessageBox.Show(err.ToString(),"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
			}
			

			MessageBox.Show("Dynamic Programming: "+DPcount+" out of 765\n"+
							"Monte Carlo: "+MCcount+" out of 765\n"+
							"Temporal Difference: "+TDcount+" out of 765\n","How many states found so far?",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
        #endregion


        #region Game Play

        /// <summary>
        /// Draw Game board and Players moves
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawGameBoard(object sender, System.Windows.Forms.PaintEventArgs e)
		{
           
				Graphics g = e.Graphics;

                //Pre-calculate
                this.ThirdWidth = pictureBox.Width / 3;
                this.ThirdHeight = pictureBox.Height / 3;
                this.TwoThirdWidth = 2 * this.ThirdWidth;
                this.TwoThirdHeight = 2 * this.ThirdHeight;
				
				//Win Location
				Brush greenBrush = new SolidBrush(Color.Green);
				Pen greenPen = new Pen(greenBrush,3.0F);
				
                //Draw #
                DrawBoardLines(g);

                //X will be blue, and O will be red
                using(Brush blueBrush = new SolidBrush(Color.Blue))
                using(Pen bluePen = new Pen(blueBrush,2.0F))
                using(Brush redBrush = new SolidBrush(Color.Red))
                using (Pen redPen = new Pen(redBrush, 2.0F))
                {
                    for (int state = 0; state <= 8; state++)
                    {
                        if (currentState[state] == (int)Player.First)
                        {
                            DrawX(g, bluePen, state);
                        }
                        else if (currentState[state] == (int)Player.Second)
                        {
                            DrawO(g, redPen, state);
                        }
                        //else do nothing
                    }
                }
                
                //Draw winning strike or cats
                DrawWin(g, greenPen, win);				
		}

        /// <summary>
        /// Draw strike if a win occurred
        /// </summary>
        /// <param name="g"></param>
        /// <param name="winPen"></param>
        /// <param name="win"></param>
        private void DrawWin(Graphics g, Pen winPen, int win)
        {
            switch (win)
            {
                case 1: //top horizontal
                    g.DrawLine(winPen, new PointF(0,ThirdHeight/2), new PointF(this.pictureBox.Width,ThirdHeight/2));
                    break;
                case 2: //middle horizontal
                    g.DrawLine(winPen, new PointF(0, (TwoThirdHeight + ThirdHeight) / 2), new PointF(this.pictureBox.Width, (TwoThirdHeight + ThirdHeight) / 2));
                    break;
                case 3: //bottom horizontal
                    g.DrawLine(winPen, new PointF(0, (TwoThirdHeight + this.pictureBox.Height) / 2), new PointF(this.pictureBox.Width, (TwoThirdHeight + this.pictureBox.Height) / 2));
                    break;
                case 4: //left vertical
                    g.DrawLine(winPen, new PointF((ThirdWidth)/ 2, 0), new PointF((ThirdWidth) / 2, this.pictureBox.Height));
                    break;
                case 5: //middle vertical
                    g.DrawLine(winPen, new PointF((ThirdWidth + TwoThirdWidth)/ 2, 0), new PointF((ThirdWidth +TwoThirdWidth) / 2, this.pictureBox.Height));
                    break;
                case 6: //right vertical
                    g.DrawLine(winPen, new PointF((this.pictureBox.Width + TwoThirdWidth) / 2, 0), new PointF((this.pictureBox.Width +TwoThirdWidth) / 2, this.pictureBox.Height));
                    break;
                case 7: //top left to bottom right diagonal
                    g.DrawLine(winPen, new PointF(0, 0), new PointF(this.pictureBox.Width, this.pictureBox.Height));
                    break;
                case 8: //bottom left to upper right
                    g.DrawLine(winPen, new PointF(0, this.pictureBox.Height), new PointF(this.pictureBox.Width, 0));
                    break;
                case -1: //cats game
                    //Cat Brush
                    using(Brush catBrush = new LinearGradientBrush(new Point(0, 0), new Point(this.pictureBox.Width, this.pictureBox.Height), Color.Red, Color.Blue))
                    using(Font font = new Font("Century Gothic", 40F))
                    {
                        g.DrawString("Cat's Game", font, catBrush, 0, ThirdHeight);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Draw O on the board
        /// </summary>
        /// <param name="g"></param>
        /// <param name="OPen"></param>
        /// <param name="position"></param>
        private void DrawO(Graphics g, Pen OPen, int position)
        {
            switch (position)
            {
                case 0:
                    g.DrawEllipse(OPen,0, 0, ThirdWidth,ThirdHeight);
                    break;
                case 1:
                    g.DrawEllipse(OPen, ThirdWidth, 0, ThirdWidth,ThirdHeight);
                    break;
                case 2:
                    g.DrawEllipse(OPen,TwoThirdWidth, 0,ThirdWidth,ThirdHeight);
                    break;
                case 3:
                    g.DrawEllipse(OPen, 0, ThirdHeight, ThirdWidth, ThirdHeight);
                    break;
                case 4:
                    g.DrawEllipse(OPen,ThirdWidth,ThirdHeight,ThirdWidth,ThirdHeight);
                    break;
                case 5:
                    g.DrawEllipse(OPen,TwoThirdWidth,ThirdHeight,ThirdWidth,ThirdHeight);
                    break;
                case 6:
                    g.DrawEllipse(OPen, 0, TwoThirdHeight,ThirdWidth,ThirdHeight);
                    break;
                case 7:
                    g.DrawEllipse(OPen, ThirdWidth, TwoThirdHeight, ThirdWidth, ThirdHeight);
                    break;
                case 8:
                    g.DrawEllipse(OPen, TwoThirdWidth, TwoThirdHeight, ThirdWidth, ThirdHeight);
                    break;
            }  
        }

        /// <summary>
        /// Draw X on the board
        /// </summary>
        /// <param name="g"></param>
        /// <param name="XPen"></param>
        /// <param name="position"></param>
        private void DrawX(Graphics g, Pen XPen, int position)
        {
            switch (position)
            {
                case 0: //upper-left corner
                    g.DrawLine(XPen, new PointF(0, 0), new PointF(ThirdWidth, ThirdHeight));
                    g.DrawLine(XPen, new PointF(0, ThirdHeight), new PointF(ThirdWidth, 0));
                    break;
                case 1: //top middle
                    g.DrawLine(XPen, new PointF(ThirdWidth,0),new PointF(TwoThirdWidth,ThirdHeight));
                    g.DrawLine(XPen, new PointF(ThirdWidth,ThirdHeight),new PointF(TwoThirdWidth,0));
                    break;
                case 2: //upper-right corner
					g.DrawLine(XPen, new PointF(TwoThirdWidth,0),new PointF(this.pictureBox.Width,ThirdHeight));
					g.DrawLine(XPen, new PointF(TwoThirdWidth,ThirdHeight), new PointF(this.pictureBox.Width,0));
                    break;
                case 3: //left middle
                    g.DrawLine(XPen, new PointF(0,ThirdHeight), new PointF(ThirdWidth,TwoThirdHeight));
					g.DrawLine(XPen, new PointF(0,TwoThirdHeight), new PointF(ThirdWidth,ThirdHeight));
                    break;
                case 4: //center
                    g.DrawLine(XPen, new PointF(ThirdWidth,ThirdHeight), new PointF(TwoThirdWidth,TwoThirdHeight));
					g.DrawLine(XPen, new PointF(ThirdWidth,TwoThirdHeight), new PointF(TwoThirdWidth,ThirdHeight));
                    break;
                case 5: //right middle
                    g.DrawLine(XPen, new PointF(TwoThirdWidth,ThirdHeight),new PointF(this.pictureBox.Width,TwoThirdHeight));
					g.DrawLine(XPen, new PointF(TwoThirdWidth,TwoThirdHeight), new PointF(this.pictureBox.Width,ThirdHeight));
                    break;
                case 6: //bottom left
                    g.DrawLine(XPen, new PointF(0,TwoThirdHeight), new PointF(ThirdWidth, this.pictureBox.Height));
                    g.DrawLine(XPen, new PointF(0,this.pictureBox.Height), new PointF(ThirdWidth,TwoThirdHeight));
                    break;
                case 7: //bottom center
                    g.DrawLine(XPen, new PointF(ThirdWidth,TwoThirdHeight), new PointF(TwoThirdWidth,this.pictureBox.Height));
                    g.DrawLine(XPen, new PointF(ThirdWidth,this.pictureBox.Height), new PointF(TwoThirdWidth,TwoThirdHeight));
                    break;
                case 8: //bottom right
                    g.DrawLine(XPen, new PointF(TwoThirdWidth,TwoThirdHeight), new PointF(this.pictureBox.Width, this.pictureBox.Height));
                    g.DrawLine(XPen, new PointF(TwoThirdWidth, this.pictureBox.Height), new PointF(this.pictureBox.Width, TwoThirdHeight));
                    break;
            }
        }

        /// <summary>
        /// Draw Game Board's #
        /// </summary>
        /// <param name="g"></param>
        private void DrawBoardLines(Graphics g)
        {
            PointF topLeftVertLinePt = new PointF(this.ThirdWidth, 0);
            PointF bottomLeftVertLinePt = new PointF(this.ThirdWidth, pictureBox.Height);
            PointF topRightVertLinePt = new PointF(this.TwoThirdWidth, 0);
            PointF bottomRightVertLinePt = new PointF(this.TwoThirdWidth, pictureBox.Height);

            PointF topLeftHorizLinePt = new PointF(0,this.ThirdHeight);
            PointF topRightHorizLinePt = new PointF(pictureBox.Width,this.ThirdHeight);
            PointF bottomLeftHorizLinePt = new PointF(0,this.TwoThirdHeight);
            PointF bottomRightHorizLinePt = new PointF(pictureBox.Width,this.TwoThirdHeight);

            using(Brush blackBrush = new SolidBrush(Color.Black))
            using (Pen blackPen = new Pen(blackBrush, 3.0F))
            {
                
                //Board Vertical Lines
                g.DrawLine(blackPen, topLeftVertLinePt, bottomLeftVertLinePt);
                g.DrawLine(blackPen, topRightVertLinePt, bottomRightVertLinePt);
                //Board Horizontal Lines
                g.DrawLine(blackPen, topLeftHorizLinePt, topRightHorizLinePt);
                g.DrawLine(blackPen, bottomLeftHorizLinePt, bottomRightHorizLinePt);

            }
        }

        /// <summary>
        /// Make User's selected move, make agent move, check for win
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int x;
            int y;
            int location;


            if ((int)gameType != 0)
            {
                #region Get Location of Click
                //checks to find what column the click was made in
                //else gives an error
                if (e.X <= ThirdWidth && e.X >= 0)
                {
                    x = 0;
                }
                else if (e.X <= TwoThirdWidth)
                {
                    x = 1;
                }
                else if (e.X <= this.pictureBox.Width)
                {
                    x = 2;
                }
                else
                {
                    x = -1;
                }

                //checks to find what row the click was made in
                //else gives an error
                if (e.Y <=ThirdHeight && e.Y >= 0)
                {
                    y = 0;
                }
                else if (e.Y <= TwoThirdHeight)
                {
                    y = 1;
                }
                else if (e.Y <= this.pictureBox.Height)
                {
                    y = 2;
                }
                else
                {
                    y = -1;
                }
                #endregion

                location =  FindUserMove(x, y);
           

                if (currentState[location] == 0 && win == 0)//check for legal move -- if space is empty and no one won
                {
                    if (turn == 0) //if it's was X 
                    {
                        currentState[location] = (int)Player.First;
                    }
                    else //turn == 1 //if it was O
                    {
                        currentState[location] = (int)Player.Second;
                    }

                    //MessageBox.Show(location.ToString());

                    movesLeft--;
                    checkWin();
                    this.pictureBox.Invalidate();
                    turn = (turn + 1) % 2;
                }
                else
                {
                    if (win == 0)
                    {
                        //A 3-4-3 Haiku, by accident. <^__^>
                        MessageBox.Show("Silly Bear, You can't move there, try else where!", "Illegal Move Attempted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else //check if user wants to play again and reinforce agent's results
                    {
                        UpdateAgentsGameVsUser();

                        //check if play again
                        playAgain(sender, e);
                    }
                }
            }

            MakeAgentsMoveAgainstUser();

            
        }

        /// <summary>
        /// Make the Agents next move against user
        /// </summary>
        private void MakeAgentsMoveAgainstUser()
        {
            //agentO
            if (this.gameType == GameType.UserXVsAgent && this.movesLeft != BOARD_SIZE && win == 0 && turn == 1)
            {
                GetMove getMove;

                if (this.menuItemEditOpponentDPAgent.Checked)
                {
                    getMove = agentDPO.getMove;
                }
                else if (this.menuItemEditOpponentMCAgent.Checked)
                {
                    getMove = agentMCO.getMove;
                }
                else //this menuItem22.Checked
                {
                    getMove = agentTDO.getMove;
                }

                this.currentState[getMove(this.currentState)] = (int)Player.Second;
                this.turn = (this.turn + 1) % 2;
                movesLeft--;
                checkWin();
                this.pictureBox.Invalidate();
            }
            //agentX
            else if (this.gameType == GameType.UserOVsAgent && win == 0 && turn == 0)
            {
                GetMove getMove;

                if (this.menuItemEditOpponentDPAgent.Checked)
                {
                    getMove = agentDPX.getMove;
                }
                else if (this.menuItemEditOpponentMCAgent.Checked)
                {
                    getMove = agentMCX.getMove;
                }
                else //this menuItem22.Checked
                {
                    getMove = agentTDX.getMove;
                }

                //agentDPX;
                this.currentState[getMove(this.currentState)] = (int)Player.First;
                this.turn = (this.turn + 1) % 2;
                movesLeft--;
                checkWin();
                this.pictureBox.Invalidate();
            }
            else
            {
            }
        }

        /// <summary>
        /// Update Agents Result When Playing Against User
        /// </summary>
        private void UpdateAgentsGameVsUser()
        {
            //agentO
            if (this.gameType == GameType.UserXVsAgent)
            {
                Update update;

                if (this.menuItemEditOpponentDPAgent.Checked)
                {
                    update = agentDPO.update;
                }
                else if (this.menuItemEditOpponentMCAgent.Checked)
                {
                    update = agentMCO.update;
                }
                else //this menuItem22.Checked
                {
                    update = agentTDO.update;
                }

                switch (winner)
                {
                    case 1://X won
                        update(GameResult.Lost, this.currentState, gameType);
                        break;
                    case 2://O won
                        update(GameResult.Win, this.currentState, gameType);
                        break;
                    default://Cat's Game
                        update(GameResult.Tie, this.currentState, gameType);
                        break;
                }
            }
            //agentX
            else if (this.gameType == GameType.UserOVsAgent)
            {
                Update update;

                if (this.menuItemEditOpponentDPAgent.Checked)
                {
                    update = agentDPX.update;
                }
                else if (this.menuItemEditOpponentMCAgent.Checked)
                {
                    update = agentMCX.update;
                }
                else //this menuItem22.Checked
                {
                    update = agentTDX.update;
                }

                switch (winner)
                {
                    case 1://X won
                        update(GameResult.Win, this.currentState, gameType);
                        break;
                    case 2://O won
                        update(GameResult.Lost, this.currentState, gameType);
                        break;
                    default://Cat's Game
                        update(GameResult.Tie, this.currentState, gameType);
                        break;
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// Get grid location of where user clicked
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private int FindUserMove(int x, int y)
        {
            //outside boundary
            if (x == -1 || y == -1)
                return -1;

            return arrayPositionReference[x, y];
        }

        /// <summary>
        /// Give Dialog for new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playAgain(object sender, MouseEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("New Game?", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                switch (gameType)
                {
                    case GameType.UserXVsAgent:
                        menuItemFileNewGameSinglePlayerAsX_Click(sender, e);
                        break;
                    case GameType.UserOVsAgent:
                        menuItemFileNewGameSinglePlayerAsO_Click(sender, e);
                        break;
                    case GameType.UserVsUser:
                        menuItemFileNewGameTwoPlayer_Click(sender, e);
                        break;
                    case GameType.AgentVsAgent:
                        menuItemFileNewGameSimulation_Click(sender, e);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //do nothing
            }
        }
        #endregion

        #region Save Results
        private void ReadCount()
		{
			try
			{
				XmlTextReader countReader = new XmlTextReader("Interplay.xml");
				countReader.ReadStartElement("count");
				count = int.Parse(countReader.ReadString());
				countReader.Close();
			}
			catch(System.IO.FileNotFoundException)
			{
				XmlTextWriter counter = new XmlTextWriter("Interplay.xml",System.Text.Encoding.Default);
				counter.WriteStartDocument(true);
				counter.WriteComment("Just a small xml, dealing with the number of times a computer vs. computer would play");
				counter.WriteStartElement("count");
				counter.WriteString("1");
				counter.WriteEndElement();
				counter.WriteEndDocument();
				counter.Close();
			}
		}

		private void checkWin()
		{
			// X wins top horizontal
            if (currentState[0] == (int)PlayerShape.X && currentState[1] == (int)PlayerShape.X && currentState[2] == (int)PlayerShape.X)
			{
				win = 1;
				winner = (int)Player.First;
				winPercentage();
			}
				// O wins top horizontal
            else if (currentState[0] == (int)PlayerShape.O && currentState[1] == (int)PlayerShape.O && currentState[2] == (int)PlayerShape.O)
			{
				win = 1;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins middle horizontal
            else if (currentState[3] == (int)PlayerShape.X && currentState[4] == (int)PlayerShape.X && currentState[5] == (int)PlayerShape.X)
			{
				win = 2;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins middle horizontal
            else if (currentState[3] == (int)PlayerShape.O && currentState[4] == (int)PlayerShape.O && currentState[5] == (int)PlayerShape.O)
			{
				win = 2;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins bottom horizontal
            else if (currentState[6] == (int)PlayerShape.X && currentState[7] == (int)PlayerShape.X && currentState[8] == (int)PlayerShape.X)
			{
				win = 3;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins bottom horizontal
            else if (currentState[6] == (int)PlayerShape.O && currentState[7] == (int)PlayerShape.O && currentState[8] == (int)PlayerShape.O)
			{
				win = 3;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins left vertical
            else if (currentState[0] == (int)PlayerShape.X && currentState[3] == (int)PlayerShape.X && currentState[6] == (int)PlayerShape.X)
			{
				win = 4;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins bottom horizontal
            else if (currentState[0] == (int)PlayerShape.O && currentState[3] == (int)PlayerShape.O && currentState[6] == (int)PlayerShape.O)
			{
				win = 4;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins middle vertical
            else if (currentState[1] == (int)PlayerShape.X && currentState[4] == (int)PlayerShape.X && currentState[7] == (int)PlayerShape.X)
			{
				win = 5;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins middle horizontal
            else if (currentState[1] == (int)PlayerShape.O && currentState[4] == (int)PlayerShape.O && currentState[7] == (int)PlayerShape.O)
			{
				win = 5;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins right vertical
            else if (currentState[2] == (int)PlayerShape.X && currentState[5] == (int)PlayerShape.X && currentState[8] == (int)PlayerShape.X)
			{
				win = 6;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins right horizontal
            else if (currentState[2] == (int)PlayerShape.O && currentState[5] == (int)PlayerShape.O && currentState[8] == (int)PlayerShape.O)
			{
				win = 6;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins top left to bottom right diagonal
            else if (currentState[0] == (int)PlayerShape.X && currentState[4] == (int)PlayerShape.X && currentState[8] == (int)PlayerShape.X)
			{
				win = 7;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins top left to bottom right diagonal
            else if (currentState[0] == (int)PlayerShape.O && currentState[4] == (int)PlayerShape.O && currentState[8] == (int)PlayerShape.O)
			{
				win = 7;
				winner = (int)Player.Second;
				winPercentage();
			}

				// X wins bottom left to top right diagonal
            else if (currentState[6] == (int)PlayerShape.X && currentState[4] == (int)PlayerShape.X && currentState[2] == (int)PlayerShape.X)
			{
				win = 8;
                winner = (int)Player.First;
				winPercentage();
			}
				// O wins bottom left to top right diagonal
            else if (currentState[6] == (int)PlayerShape.O && currentState[4] == (int)PlayerShape.O && currentState[2] == (int)PlayerShape.O)
			{
				win = 8;
				winner = (int)Player.Second;
				winPercentage();
			}
			else if(currentState[0] != 0 && currentState[1] != 0 && currentState[2] != 0 &&
					currentState[3] != 0 && currentState[4] != 0 && currentState[5] != 0 &&
					currentState[6] != 0 && currentState[7] != 0 && currentState[8] != 0)
			{
				win = -1;
				winner = 0;
				winPercentage();
			}
			else
			{
				//do nothing game not over yet
			}
		}

		private void winPercentage()
		{
			//Read info
			/* <Win>
			 * <DPX>
			 * <Won></Won>
			 * <Played></Played>
			 * </DPX>
			 * ...
			 * </Win>
			 */
			int [,] data = new int[2,8];
			
			try
			{
				XmlTextReader win = new XmlTextReader("WinPercent.xml");
				
				win.ReadStartElement("Win");
				
				win.ReadStartElement("DPX");
				win.ReadStartElement("Won");
				data[0,0] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,0] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("DPO");
				win.ReadStartElement("Won");
				data[0,1] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,1] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();
		
				win.ReadStartElement("MCX");
				win.ReadStartElement("Won");
				data[0,2] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,2] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("MCO");
				win.ReadStartElement("Won");
				data[0,3] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,3] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("TDX");
				win.ReadStartElement("Won");
				data[0,4] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,4] = int.Parse(win.ReadString());	
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("TDO");
				win.ReadStartElement("Won");
				data[0,5] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,5] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("UserX");
				win.ReadStartElement("Won");
				data[0,6] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,6] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.ReadStartElement("UserO");
				win.ReadStartElement("Won");
				data[0,7] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadStartElement("Played");
				data[1,7] = int.Parse(win.ReadString());
				win.ReadEndElement();
				win.ReadEndElement();

				win.Close();

			}
			catch(System.IO.FileNotFoundException)
			{
				for(int i = 0; i < 2; i++)
				{
					for(int j = 0; j<8; j++)
					{
						data[i,j] = 0;
					}
				}
			}
			//update info
			switch(gameType)
			{
				case GameType.UserXVsAgent: //User as X
					if(this.menuItemEditOpponentDPAgent.Checked)
					{
						if(winner == (int)Player.First)
						{
							data[1,1] = data[1,1] + 1;
							data[0,6] = data[0,6] + 1;
							data[1,6] = data[1,6] + 1;
						}
						else if(winner == (int)Player.Second)
						{
							data[1,6] = data[1,6] + 1;
							data[0,1] = data[0,1] + 1;
							data[1,1] = data[1,1] + 1;
						}
						else
						{
							data[1,6] = data[1,6] + 1;
							data[1,1] = data[1,1] + 1;
						}
					}
					else if(this.menuItemEditOpponentMCAgent.Checked)
					{
                        if (winner == (int)Player.First)
						{
							data[1,3] = data[1,3] + 1;
							data[0,6] = data[0,6] + 1;
							data[1,6] = data[1,6] + 1;
						}
                        else if (winner == (int)Player.Second)
						{
							data[1,6] = data[1,6] + 1;
							data[0,3] = data[0,3] + 1;
							data[1,3] = data[1,3] + 1;
						}
						else
						{
							data[1,6] = data[1,6] + 1;
							data[1,3] = data[1,3] + 1;
						}
					}
					else
					{
						if(winner == (int)Player.First)
						{
							data[1,5] = data[1,5] + 1;
							data[0,6] = data[0,6] + 1;
							data[1,6] = data[1,6] + 1;
						}
                        else if (winner == (int)Player.Second)
						{
							data[1,6] = data[1,6] + 1;
							data[0,5] = data[0,5] + 1;
							data[1,5] = data[1,5] + 1;
						}
						else
						{
							data[1,6] = data[1,6] + 1;
							data[1,5] = data[1,5] + 1;
						}
					}
					break;
				case GameType.UserOVsAgent: //User as O
					if(this.menuItemEditOpponentDPAgent.Checked)
					{
                        if (winner == (int)Player.Second)
						{
							data[1,0] = data[1,0] + 1;
							data[0,7] = data[0,7] + 1;
							data[1,7] = data[1,7] + 1;
						}
                        else if (winner == (int)Player.First)
						{
							data[1,7] = data[1,7] + 1;
							data[0,0] = data[0,0] + 1;
							data[1,0] = data[1,0] + 1;
						}
						else
						{
							data[1,7] = data[1,7] + 1;
							data[1,0] = data[1,0] + 1;
						}
					}
					else if(this.menuItemEditOpponentMCAgent.Checked)
					{
                        if (winner == (int)Player.Second)
						{
							data[1,2] = data[1,2] + 1;
							data[0,7] = data[0,7] + 1;
							data[1,7] = data[1,7] + 1;
						}
                        else if (winner == (int)Player.First)
						{
							data[1,7] = data[1,7] + 1;
							data[0,2] = data[0,2] + 1;
							data[1,2] = data[1,2] + 1;
						}
						else
						{
							data[1,7] = data[1,7] + 1;
							data[1,2] = data[1,2] + 1;
						}
					}
					else
					{
                        if (winner == (int)Player.Second)
						{
							data[1,4] = data[1,4] + 1;
							data[0,7] = data[0,7] + 1;
							data[1,7] = data[1,7] + 1;
						}
                        else if (winner == (int)Player.First)
						{
							data[1,7] = data[1,7] + 1;
							data[0,4] = data[0,4] + 1;
							data[1,4] = data[1,4] + 1;
						}
						else
						{
							data[1,7] = data[1,7] + 1;
							data[1,4] = data[1,4] + 1;
						}
					}
					break;
				case GameType.UserVsUser: //Two Users
                    if (winner == (int)Player.First)
					{
						data[1,7] = data[1,7] + 1;
						data[0,6] = data[0,6] + 1;
						data[1,6] = data[1,6] + 1;
					}
                    else if (winner == (int)Player.Second)
					{
						data[1,6] = data[1,6] + 1;
						data[0,7] = data[0,7] + 1;
						data[1,7] = data[1,7] + 1;
					}
					else
					{
						data[1,6] = data[1,6] + 1;
						data[1,7] = data[1,7] + 1;
					}
					break;
				case GameType.AgentVsAgent: //Two Agents
					if(this.menuItemEditOpponentDPAgent.Checked)
					{
                        if (winner == (int)Player.First)
						{
							if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[1,3] = data[1,3] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[1,5] = data[1,5] + 1;
							}
							else//(this.menuItem33.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}

							data[1,0] = data[1,0] + 1;
							data[0,0] = data[0,0] + 1;
						}
                        else if (winner == (int)Player.Second)
						{
							data[1,0] = data[1,0] + 1;
							if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[0,3] = data[0,3] + 1;
								data[1,3] = data[1,3] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[0,5] = data[0,5] + 1;
								data[1,5] = data[1,5] + 1;
							}
							else//(this.menuItem33.Checked)
							{
								data[0,1] = data[0,1] + 1;
								data[1,1] = data[1,1] + 1;
							}
						}
						else
						{
							if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[1,3] = data[1,3] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[1,5] = data[1,5] + 1;
							}
							else//(this.menuItem33.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}
							
							data[1,0] = data[1,0] + 1;
						}
					}
					else if(this.menuItemEditOpponentMCAgent.Checked)
					{
                        if (winner == (int)Player.First)
						{
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[1,5] = data[1,5] + 1;
							}
							else
							{
								data[1,3] = data[1,3] + 1;
							}
							data[0,2] = data[0,2] + 1;
							data[1,2] = data[1,2] + 1;
						}
                        else if (winner == (int)Player.Second)
						{
							data[1,2] = data[1,2] + 1;
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
								data[0,1] = data[0,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[1,5] = data[1,5] + 1;
								data[0,5] = data[0,5] + 1;
							}
							else
							{
								data[0,3] = data[0,3] + 1;
								data[1,3] = data[1,3] + 1;
							}
						}
						else
						{
							data[1,2] = data[1,2] + 1;
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstTD.Checked)
							{
								data[1,5] = data[1,5] + 1;
							}
							else
							{
								data[1,3] = data[1,3] + 1;
							}
						}
					}
					else //TDX vs TDO
					{
                        if (winner == (int)Player.First)
						{
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[1,3] = data[1,3] + 1;
							}
							else
							{
								data[1,5] = data[1,5] + 1;
							}

							data[0,4] = data[0,4] + 1;
							data[1,4] = data[1,4] + 1;
						}
                        else if (winner == (int)Player.Second)
						{
							data[1,4] = data[1,4] + 1;
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
								data[0,1] = data[0,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[1,3] = data[1,3] + 1;
								data[0,3] = data[0,3] + 1;
							}
							else
							{
								data[0,5] = data[0,5] + 1;
								data[1,5] = data[1,5] + 1;
							}

						}
						else
						{
							if(this.menuItemFileNewGameSimulationAgainstDP.Checked)
							{
								data[1,1] = data[1,1] + 1;
							}
							else if(this.menuItemFileNewGameSimulationAgainstMC.Checked)
							{
								data[1,3] = data[1,3] + 1;
							}
							else
							{
								data[1,5] = data[1,5] + 1;
							}
							data[1,4] = data[1,4] + 1;
						}
					}
					break;
			}


			//write info
			XmlTextWriter winXml = new XmlTextWriter("WinPercent.xml",System.Text.Encoding.Default);
			winXml.WriteStartDocument(true);
			winXml.WriteComment("This keeps track of the win percentage of each game played");
				
			winXml.WriteStartElement("Win");
				
			winXml.WriteStartElement("DPX");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,0].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,0].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("DPO");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,1].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,1].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();
		
			winXml.WriteStartElement("MCX");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,2].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,2].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("MCO");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,3].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,3].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("TDX");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,4].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,4].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("TDO");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,5].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,5].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("UserX");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,6].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,6].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteStartElement("UserO");
			winXml.WriteStartElement("Won");
			winXml.WriteString(data[0,7].ToString());
			winXml.WriteEndElement();
			winXml.WriteStartElement("Played");
			winXml.WriteString(data[1,7].ToString());
			winXml.WriteEndElement();
			winXml.WriteEndElement();

			winXml.WriteEndElement();
			winXml.WriteEndDocument();
			
			winXml.Close();
        }
        #endregion






    }
}
