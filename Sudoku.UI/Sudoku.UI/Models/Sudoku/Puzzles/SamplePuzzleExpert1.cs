using Sudoku.UI.Services;
using System.Collections.Generic;

namespace Sudoku.UI.Models.Sudoku.Puzzles
{
    public class SamplePuzzleExpert1 : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
               9,6,null,3,null,null,8,null,1,
               null,null,null,null,2,null,3,null,null,
               4,null,null,null,null,null,null,null,null,
               5,null,null,null,null,null,null,2,7,
               null,null,9,null,null,4,null,null,null,
               null,null,null,6,8,null,null,null,null,
               null,null,null,1,6,2,null,9,8,
               null,5,null,4,null,null,1,null,null,
               null,null,null,null,null,null,null,null,null
            };
        }
    }
}
