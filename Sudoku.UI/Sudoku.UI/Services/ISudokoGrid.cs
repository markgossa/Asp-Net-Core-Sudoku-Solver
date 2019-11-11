using Sudoku.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Services
{
    public interface ISudokoGrid
    {
        List<SudokuCell> Cells { get; set; }
    }
}
