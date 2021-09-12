using Minesweeper.Common;
using Minesweeper.ConsoleManager;
using Minesweeper.Models;
using Minesweeper.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Program
    {
        private static Validator validator;
        private static ConsoleWriter consoleWriter;

        static void Main(string[] args)
        {
            SetGame();
        }

        public static void SetGame()
        {
            SetDefaultValues();
            SetBombLocations();
            consoleWriter.DrawGame();
            GetInputAndPlay();
        }

        /// <summary>
        /// Initialize default game values
        /// </summary>
        private static void SetDefaultValues()
        {
            validator = new Validator();
            consoleWriter = new ConsoleWriter();
            Constants.GameLocations = new List<Positions>();
            Constants.IsGameFinished = false;
            Constants.remainingLives = 5;
            Constants.currentScore = 0;
            Constants.gameHeight = 10;
            Constants.gameWidth = 30;
            Constants.currentX = 0;
            Constants.currentY = 5;
        }

        /// <summary>
        /// Randomnly set Bomb locations
        /// </summary>
        private static void SetBombLocations()
        {
            Positions GamePositions = null;
            Random rnd = new Random();
            for (int row = 0; row < Constants.gameHeight; row++)
            {
                int bomb1Column = rnd.Next(0, Constants.gameWidth);                                                          //Randomly assign 3 bombs in each row
                int bomb2Column = rnd.Next(0, Constants.gameWidth);
                int bomb3Column = rnd.Next(0, Constants.gameWidth);

                for (int column = 0; column < Constants.gameWidth; column++)
                {
                    GamePositions = new Positions { Row = row, Column = column };

                    if (bomb1Column == column || bomb2Column == column || bomb3Column == column)
                        GamePositions.IsBomb = true;                                                                //Set postition as a bomb
                    if (Constants.currentX == column && Constants.currentY == row)
                        GamePositions.IsVisible = true;                                                             //Show if its starting point

                    Constants.GameLocations.Add(GamePositions);                                                               //Add a spot on game
                }
            };
        }

        /// <summary>
        /// Wait for user to input decision and pass it to logic methods
        /// </summary>
        public static void GetInputAndPlay()
        {
            var key = Console.ReadKey().Key;                                                                        //Get the key info from user

            bool IsAllowed = validator.CheckIfMoveAllowed(key);

            if (!IsAllowed)                                                                                         //If move is not allowed deduct life
                Constants.remainingLives--;
            else                                                                                                    //If move allowed then increase score
                Constants.currentScore++;

            if (Constants.remainingLives == 0)                                                                      //Message if users lost the game
            {
                consoleWriter.ShowDefeatMessage();
                SetGame();
            }

            if (Constants.currentX == Constants.gameWidth - 1)                                                      //Message if user wins the game
            {
                consoleWriter.ShowVictoryMessage();
                SetGame();
            }

            if (IsAllowed)
                UpdatePosition(key);

            while (!Constants.IsGameFinished)
            {
                consoleWriter.DrawGame();
                GetInputAndPlay();
            }
        }

        /// <summary>
        /// Update user's position after decision
        /// </summary>
        public static void UpdatePosition(ConsoleKey key)                                                           //Update user's positions method that sets the cursor to new position
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (Constants.currentX > 0)
                        Constants.currentX -= 1;
                    break;

                case ConsoleKey.RightArrow:
                    if (Constants.currentX < Constants.gameWidth - 1)
                        Constants.currentX += 1;
                    break;

                case ConsoleKey.UpArrow:
                    if (Constants.currentY > 0)
                        Constants.currentY -= 1;
                    break;

                case ConsoleKey.DownArrow:
                    if (Constants.currentY < Constants.gameHeight - 1)
                        Constants.currentY += 1;
                    break;
            }
        }
    }
}
