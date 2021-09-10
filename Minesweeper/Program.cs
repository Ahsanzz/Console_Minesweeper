using Minesweeper.Common;
using Minesweeper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Program
    {
        private static int gameHeight, gameWidth, currentX, currentY, currentScore, remainingLives;
        private static List<Positions> GameLocations;
        static void Main(string[] args)
        {
            SetGame();
        }

        private static void SetGame()
        {
            SetDefaultValues();
            SetBombLocations();
            DrawGame();
        }

        /// <summary>
        /// Initialize default game values
        /// </summary>
        private static void SetDefaultValues()
        {
            GameLocations = new List<Positions>();
            remainingLives = 5;
            currentScore = 0;
            gameHeight = 10;
            gameWidth = 30;
            currentX = 0;
            currentY = 5;
        }

        /// <summary>
        /// Randomnly set Bomb locations
        /// </summary>
        private static void SetBombLocations()
        {
            Positions GamePositions = null;
            Random rnd = new Random();
            for (int row = 0; row < gameHeight; row++)
            {
                int bomb1Column = rnd.Next(0, gameWidth);                                                          //Randomly assign 3 bombs in each row
                int bomb2Column = rnd.Next(0, gameWidth);
                int bomb3Column = rnd.Next(0, gameWidth);

                for (int column = 0; column < gameWidth; column++)
                {
                    GamePositions = new Positions { Row = row, Column = column };

                    if (bomb1Column == column || bomb2Column == column || bomb3Column == column)
                        GamePositions.IsBomb = true;                                                                //Set postition as a bomb
                    if (currentX == column && currentY == row)
                        GamePositions.IsVisible = true;                                                             //Show if its starting point

                    GameLocations.Add(GamePositions);                                                               //Add a spot on game
                }
            };
        }

        /// <summary>
        /// Draw game on console
        /// </summary>
        private static void DrawGame()
        {
            Console.Clear();                                                                                        //Clear console before drawing
            for (int row = 0; row < gameHeight; row++)
            {
                for (int column = 0; column < gameWidth; column++)
                {
                    if (CheckCurrentPosition(row, column).IsBomb && CheckCurrentPosition(row, column).IsVisible)
                        Console.Write("x");                                                                         //Draw x if we hit this spot and its a bomb
                    else if (CheckCurrentPosition(row, column).IsVisible)
                        Console.Write("-");                                                                         //Draw - if this spot is hit and its empty
                    else
                        Console.Write("O");                                                                         //draw 0 is this spot is not hit yet
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
            Console.WriteLine($" Score: {currentScore}  Lives: {remainingLives}  Position: {Constants.ColumnValues[currentY]}{currentX}"); //Display lives and current score
            Console.WriteLine("----------------------------------");
            Console.WriteLine();
            Console.WriteLine("Use arrow keys to play.");

            Console.SetCursorPosition(currentX, currentY);                                                          //Sets cursor position
            GetInputAndPlay();                                                                                      //Wait for the next input
        }

        /// <summary>
        /// Wait for user to input decision and pass it to logic methods
        /// </summary>
        private static void GetInputAndPlay()
        {
            var key = Console.ReadKey().Key;                                                                        //Get the key info from user

            if (CheckIfMoveAllowed(key))                                                                            //To check if move is allowed before updating user's position 
                UpdatePosition(key);
            //Call Draw Method
            DrawGame();
        }

        /// <summary>
        /// Logic method to check if the move is allowed or user hit a bomb
        /// </summary>
        private static bool CheckIfMoveAllowed(ConsoleKey key)
        {
            bool IsAllowed = true;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (currentX > 0)
                        if (CheckCurrentPosition(currentY, currentX - 1, true).IsBomb)
                            IsAllowed = false;
                    break;

                case ConsoleKey.RightArrow:
                    if (currentX < gameWidth - 1)
                        if (CheckCurrentPosition(currentY, currentX + 1, true).IsBomb)
                            IsAllowed = false;
                    break;

                case ConsoleKey.UpArrow:
                    if (currentY > 0)
                        if (CheckCurrentPosition(currentY - 1, currentX, true).IsBomb)
                            IsAllowed = false;
                    break;

                case ConsoleKey.DownArrow:
                    if (currentY < gameHeight - 1)
                        if (CheckCurrentPosition(currentY + 1, currentX, true).IsBomb)
                            IsAllowed = false;
                    break;

                default:
                    return true;
            }
            //If move is not allowed deduct life
            if (!IsAllowed)
                remainingLives--;
            else                                                                                   //If move allowed then increase score
                currentScore++;

            if (remainingLives == 0)                                                               //Message if users lost the game
            {
                Console.Clear();
                Console.WriteLine("Sorry! you lost.");
                Console.WriteLine($"Your score is {currentScore}.");
                Console.WriteLine();
                Console.WriteLine("Press enter to play again");
                Console.ReadLine();
                SetGame();
            }

            if (currentX == gameWidth - 1)                                                         //Message if user wins the game
            {
                Console.Clear();
                Console.WriteLine("Congrats! you won.");
                Console.WriteLine($"Your score is {currentScore}.");
                Console.WriteLine();
                Console.WriteLine("Press enter to play again");
                Console.ReadLine();
                SetGame();
            }

            return IsAllowed;
        }

        /// <summary>
        /// Update user's position after decision
        /// </summary>
        private static void UpdatePosition(ConsoleKey key)                                         //Update user's positions method that sets the cursor to new position
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (currentX > 0)
                        currentX -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    if (currentX < gameWidth - 1)
                        currentX += 1;
                    break;

                case ConsoleKey.UpArrow:
                    if (currentY > 0)
                        currentY -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (currentY < gameHeight - 1)
                        currentY += 1;
                    break;
            }
        }

        /// <summary>
        /// Return information about current position
        /// </summary>
        private static Positions CheckCurrentPosition(int row, int column, bool makeVisible = false)        //Return info about current position that user moved to
        {
            var positions = new Positions();
            positions = GameLocations.Where(x => x.Row == row && x.Column == column).FirstOrDefault();
            if (positions != null)
            {
                if (makeVisible)                                                                            //Also makes the position visible on console
                    positions.IsVisible = true;
                return positions;
            }
            else
                return positions;
        }

    }
}
