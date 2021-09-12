using Minesweeper.Common;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Validators
{
    public class Validator
    {
        public bool IsBomb(int row, int column, bool makeVisible = false)
        {
            var positions = new Positions();
            positions = Constants.GameLocations.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
            if (positions != null)
            {
                if (makeVisible)                                                                            //Also makes the position visible on console
                    positions.IsVisible = true;

                return positions.IsBomb;
            }
            else
                return false;
        }

        public bool IsVisibile(int row, int column)
        {
            var positions = new Positions();
            positions = Constants.GameLocations.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
            if (positions != null)
            {
                return positions.IsVisible;
            }
            else
                return false;
        }

        public bool CheckIfMoveAllowed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (Constants.currentX > 0)
                        return !IsBomb(Constants.currentY, Constants.currentX - 1, true);
                    break;

                case ConsoleKey.RightArrow:
                    if (Constants.currentX < Constants.gameWidth - 1)
                        return !IsBomb(Constants.currentY, Constants.currentX + 1, true);
                    break;

                case ConsoleKey.UpArrow:
                    if (Constants.currentY > 0)
                        return !IsBomb(Constants.currentY - 1, Constants.currentX, true);
                    break;

                case ConsoleKey.DownArrow:
                    if (Constants.currentY < Constants.gameHeight - 1)
                        return !IsBomb(Constants.currentY + 1, Constants.currentX, true);
                    break;

                default:
                    return true;
            }
            return true;
        }
    }
}
