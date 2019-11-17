using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku.Puzzles
{
    public class SamplePuzzleEasy : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                null, 7, 4, null, null, null, null, 9, null,
                2, null, 6, null, 4, null, 8 ,7, null,
                null, null, 1, null, null, 3, 2, null, null,
                null, null, null, 1, null, 4, 7, null, null,
                null, 1, null, 6, 9, 7, null, 2, null,
                null, null, 5, 3, null, 8, null, null, null,
                null, null, 9, 7, null, null, 4, null, null,
                null, 4, 7, null, 6, null, 3, null, 2,
                null, 5, null, null, null, null, 9, 6, null
            };
        }
    }
}
