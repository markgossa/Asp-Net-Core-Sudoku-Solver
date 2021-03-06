﻿using Sudoku.UI.Tests.Services;
using System.Collections.Generic;

namespace Sudoku.UI.Tests.Models.Puzzles
{
    public class WorldsHardestPuzzle : IPuzzle
    {
        public List<int?> GetPuzzle()
        {
            return new List<int?>()
            {
                8, null, null, null, null, null, null, null, null,
                null, null, 3, 6, null,null,null,null,null,
                null, 7, null, null, 9, null, 2, null, null,
                null, 5, null, null, null, 7, null, null, null,
                null, null, null, null, 4, 5, 7, null, null,
                null, null, null, 1, null, null, null, 3, null,
                null, null, 1, null, null, null, null, 6, 8,
                null, null, 8, 5, null, null, null, 1, null,
                null, 9, null, null, null, null, 4, null, null
            };
        }
    }
}
