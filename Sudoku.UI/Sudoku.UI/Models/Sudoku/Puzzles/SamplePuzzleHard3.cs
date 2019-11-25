using Sudoku.UI.Services;
using System.Collections.Generic;

namespace Sudoku.UI.Models.Sudoku.Puzzles
{
    public class SamplePuzzleHard3 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                4,null,null,2,null,null,5,null,null,
                null,1,null,null,null,null,null,null,6,
                2,null,null,null,4,null,null,7,null,
                null,2,null,null,null,null,null,null,7,
                null,null,6,9,3,7,8,null,null,
                7,null,null,null,null,null,null,1,null,
                null,8,null,null,1,null,null,null,5,
                5,null,null,null,null,null,null,9,null,
                null,null,7,null,null,3,null,null,1
            };
        }
    }
}
