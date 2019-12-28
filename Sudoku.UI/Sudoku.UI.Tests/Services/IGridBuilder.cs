using Sudoku.UI.Models.Sudoku;

namespace Sudoku.UI.Tests.Services
{
    public interface IGridBuilder
    {
        Grid GetSudokuGrid();
    }
}