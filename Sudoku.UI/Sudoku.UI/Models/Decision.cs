using Sudoku.UI.Models.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class Decision
    {
        public Cell FinalCell { get; }
        public List<int> InitialPossibleValues { get; }

        public Decision(Cell cell, List<int> initialPossibleValues)
        {
            FinalCell = cell;
            InitialPossibleValues = initialPossibleValues;
        }
    }
}
