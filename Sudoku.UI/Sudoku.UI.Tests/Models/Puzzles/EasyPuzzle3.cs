using Sudoku.UI.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Tests.Models.Puzzles
{
    public class EasyPuzzle3 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                null, null, 2, 7, null, null, null, null, null,
                null, null, null, null, null, 8, null, null, 2,
                8, null, null, 1, 3, null, 5, 9, null,
                6, null, null, null, null, null, 8, null, 4,
                null, null, null, 9, null, 5, null, null, null,
                1, null, 8, null, null, null, null, null, 3,
                null, 8, 7, null, 5, 6, null, null, 9,
                5, null, null, 3, null, null, null, null, null,
                null, null, null, null, null, 9, 3, null, null
            };
        }
    }
}
