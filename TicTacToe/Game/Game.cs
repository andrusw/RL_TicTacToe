using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Game
{
    public enum GameType { UserXVsAgent = 1, UserOVsAgent = 2, UserVsUser = 3, AgentVsAgent = 4 };
    public enum Player { First = 1, Second = 2 };
    public enum PlayerShape { X = 1, O = 2 };
    public enum GameResult { Lost = -1, Tie = 0, Win = 1 };
}
