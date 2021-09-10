using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Models
{
    public class Positions
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public bool IsVisible { get; set; } = false;
        public bool IsBomb { get; set; } = false;
    }
}
