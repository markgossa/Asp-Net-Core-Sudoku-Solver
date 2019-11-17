using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku.Puzzles
{
    public class EmptyPuzzle : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>();
        }
    }
}
