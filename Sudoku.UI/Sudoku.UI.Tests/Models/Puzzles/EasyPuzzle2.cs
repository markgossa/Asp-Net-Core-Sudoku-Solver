using Sudoku.UI.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Tests.Models.Puzzles
{
    public class EasyPuzzle2 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                null, 6, null, null, 8, null, 4, 2, null,
                null, 1, 5, null, 6, null, 3, 7, 8,
                null, null, null, 4, null, null, null, 6, null,
                1, null, null, 6, null, 4, 8, 3, null,
                3, null, 6, null, 1, null, 7, null, 5,
                null, 8, null, 3, 5, null, null, null, null,
                8, 3, null, 9, 4, null, null, null, null,
                null, 7, 2, 1, 3, null, 9, null, null,
                null, null, 9, null, 2, null, 6, 1, null
            };
        }
    }
}
