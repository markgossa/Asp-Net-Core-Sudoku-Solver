﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class SudokuCell : SudokuCellCoordinate
    {
        public int Value { get; set; }
    }
}
