using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku
{
    public class Grid : IGrid
    {
        public List<Cell> Cells { get; set; }

        public List<Box> Boxes { get; set; }
    }
}
