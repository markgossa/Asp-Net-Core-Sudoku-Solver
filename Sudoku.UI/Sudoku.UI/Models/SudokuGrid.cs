﻿using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class SudokuGrid : ISudokoGrid
    {
        public List<SudokuCell> Cells { get; set; }
    }
}
