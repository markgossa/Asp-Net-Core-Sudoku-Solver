using System.Collections.Generic;

namespace Sudoku.UI.Services
{
    public interface IPuzzle
    {
        List<int?> GetPuzzle();
    }
}