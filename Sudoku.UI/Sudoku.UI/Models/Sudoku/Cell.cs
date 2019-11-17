using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku
{
    public class Cell
    {
        public int? Value { get; set; }
        public List<int> PossibleValues { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
