using Sudoku.UI.Models.Sudoku;
using System.Collections.Generic;

namespace Sudoku.UI.Services
{
    public interface IGrid
    {
        List<Cell> Cells { get; set; }
    }
}
