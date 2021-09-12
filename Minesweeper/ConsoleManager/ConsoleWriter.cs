using Minesweeper.Common;
using Minesweeper.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ConsoleManager
{
    public class ConsoleWriter
    {
        private Validator validator = new Validator();

        public void DrawGame()
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();                                                                                        //Clear console before drawing

            for (int row = 0; row < Constants.gameHeight; row++)
            {
                for (int column = 0; column < Constants.gameWidth; column++)
                {

                    if (validator.IsBomb(row, column) && validator.IsVisibile(row, column))
                        Console.Write("x");                                                                         //Draw x if we hit this spot and its a bomb
                    else if (validator.IsVisibile(row, column))
                        Console.Write("-");                                                                         //Draw - if this spot is hit and its empty
                    else
                        Console.Write("O");                                                                         //draw 0 is this spot is not hit yet
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine($" Score: {Constants.currentScore}  Lives: {Constants.remainingLives}  Position: {Constants.ColumnValues[Constants.currentY]}{Constants.currentX}"); //Display lives and current score
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Console.WriteLine("Use arrow keys to play.");

            if (!Console.IsOutputRedirected)
                Console.SetCursorPosition(Constants.currentX, Constants.currentY);                                  //Sets cursor position
        }

        public void ShowVictoryMessage()
        {
            Console.Clear();
            Console.WriteLine("Congrats! you won.");
            Console.WriteLine($"Your score is {Constants.currentScore}.");
            Console.WriteLine();
            Console.WriteLine("Press enter to play again");
            Console.ReadLine();
        }

        public void ShowDefeatMessage()
        {
            Console.Clear();
            Console.WriteLine("Sorry! you lost.");
            Console.WriteLine($"Your score is {Constants.currentScore}.");
            Console.WriteLine();
            Console.WriteLine("Press enter to play again");
            Console.ReadLine();
        }

    }
}
