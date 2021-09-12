using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minesweeper.Common;
using Minesweeper.Models;
using System;
using System.Collections.Generic;

namespace UnitTestMinesweeper
{
    [TestClass]
    public class UnitTest1
    {
        Minesweeper.Validators.Validator validator = new Minesweeper.Validators.Validator();
        Minesweeper.ConsoleManager.ConsoleWriter consoleWriter = new Minesweeper.ConsoleManager.ConsoleWriter();

        [TestMethod]
        public void BombValidatorTest()
        {
            Constants.GameLocations = new List<Positions>
            {
                new Positions { Row = 0, Column = 5, IsBomb = false, IsVisible = false },
                new Positions { Row = 1, Column = 4, IsBomb = true, IsVisible = false },
                new Positions { Row = 2, Column = 3, IsBomb = true, IsVisible = true },
                new Positions { Row = 3, Column = 2, IsBomb = false, IsVisible = false },
                new Positions { Row = 4, Column = 1, IsBomb = true, IsVisible = true },
                new Positions { Row = 5, Column = 0, IsBomb = false, IsVisible = false },
            };

            validator.IsBomb(1, 3);
        }

        [TestMethod]
        public void VisibilityValidatorTest()
        {
            Constants.GameLocations = new List<Positions>
            {
                new Positions { Row = 0, Column = 5, IsBomb = false, IsVisible = false },
                new Positions { Row = 1, Column = 4, IsBomb = true, IsVisible = false },
                new Positions { Row = 2, Column = 3, IsBomb = true, IsVisible = true },
                new Positions { Row = 3, Column = 2, IsBomb = false, IsVisible = false },
                new Positions { Row = 4, Column = 1, IsBomb = true, IsVisible = true },
                new Positions { Row = 5, Column = 0, IsBomb = false, IsVisible = false },
            };
            validator.IsVisibile(3, 5);
        }

        [TestMethod]
        public void MoveCheckerTest()
        {
            Constants.GameLocations = new List<Positions>
            {
                new Positions { Row = 0, Column = 5, IsBomb = false, IsVisible = false },
                new Positions { Row = 1, Column = 4, IsBomb = true, IsVisible = false },
                new Positions { Row = 2, Column = 3, IsBomb = true, IsVisible = true },
                new Positions { Row = 3, Column = 2, IsBomb = false, IsVisible = false },
                new Positions { Row = 4, Column = 1, IsBomb = true, IsVisible = true },
                new Positions { Row = 5, Column = 0, IsBomb = false, IsVisible = false },
            };
            validator.CheckIfMoveAllowed(ConsoleKey.UpArrow);
            validator.CheckIfMoveAllowed(ConsoleKey.DownArrow);
            validator.CheckIfMoveAllowed(ConsoleKey.LeftArrow);
            validator.CheckIfMoveAllowed(ConsoleKey.RightArrow);
        }

        [TestMethod]
        public void PositionUpdatorTest()
        {
            Constants.GameLocations = new List<Positions>
            {
                new Positions { Row = 0, Column = 5, IsBomb = false, IsVisible = false },
                new Positions { Row = 1, Column = 4, IsBomb = true, IsVisible = false },
                new Positions { Row = 2, Column = 3, IsBomb = true, IsVisible = true },
                new Positions { Row = 3, Column = 2, IsBomb = false, IsVisible = false },
                new Positions { Row = 4, Column = 1, IsBomb = true, IsVisible = true },
                new Positions { Row = 5, Column = 0, IsBomb = false, IsVisible = false },
            };
            Minesweeper.Program.UpdatePosition(ConsoleKey.UpArrow);
            Minesweeper.Program.UpdatePosition(ConsoleKey.DownArrow);
            Minesweeper.Program.UpdatePosition(ConsoleKey.LeftArrow);
            Minesweeper.Program.UpdatePosition(ConsoleKey.RightArrow);
        }
    }
}
