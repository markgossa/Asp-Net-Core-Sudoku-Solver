using Sudoku.UI.Models.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Services
{
    public interface ISolver
    {
        IGrid Solve();
    }
}
