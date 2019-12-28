using System.Collections.Generic;

namespace Sudoku.UI.Tests.Services
{
    public interface IPuzzle
    {
        List<int?> GetPuzzle();
    }
}