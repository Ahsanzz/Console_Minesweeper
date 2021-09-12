using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Common
{
    public class Constants
    {
        public static readonly List<string> ColumnValues = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public static int gameHeight, gameWidth;
        public static int currentX, currentY, currentScore, remainingLives;
        public static List<Positions> GameLocations;
        public static bool IsGameFinished;
    }
}
