using Sudoku.UI.Tests.Services;
using System.Collections.Generic;

namespace Sudoku.UI.Tests.Models.Puzzles
{
    public class EasyPuzzle1 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                null,null,8,2,null,null,9,null,3,
                3,4,2,null,9,5,null,null,7,
                1,9,7,null,null,null,null,null,4,
                null,null,5,3,1,2,4,7,9,
                null,null,null,null,null,null,null,null,null,
                2,null,null,null,7,4,5,null,null,
                null,2,null,null,null,1,null,null,5,
                null,7,null,null,null,6,8,9,1,
                8,null,null,4,3,null,7,null,6
            };
        }
    }
}
