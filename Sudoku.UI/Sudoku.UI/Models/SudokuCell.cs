using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class SudokuCell
    {
        public int Value { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
    }
}
