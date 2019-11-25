using Sudoku.UI.Services;
using System.Collections.Generic;

namespace Sudoku.UI.Models.Sudoku.Puzzles
{
    public class SamplePuzzleHard2 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                8, null, null,null,null,3,null,9,null,
                null,null,null,4,null,null,null,null,1,
                null,null,4,null,null,null,7,2,3,
                1,4,2,null,null,null,null,null,7,
                null,null,null,null,5,null,null,null,null,
                5,null,null,null,null,null,6,4,8,
                2,9,6,null,null,null,3,null,null,
                3,null,null,null,null,6,null,null,null,
                null,1,null,3,null,null,null,null,2
            };
        }
    }
}
