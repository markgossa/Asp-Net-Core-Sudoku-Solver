using Sudoku.UI.Models.Sudoku;

namespace Sudoku.UI.Services
{
    public interface IGridBuilder
    {
        Grid GetSudokuGrid();
    }
}