using Sudoku.UI.Models;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class SudokuGridBuilder : ISudokuGridBuilder
    {
        private readonly SudokuGrid _sudokoGrid;

        public SudokuGridBuilder()
        {
            _sudokoGrid = new SudokuGrid();
            _sudokoGrid.Cells = new List<SudokuCell>();
        }

        private void AddCells()
        {
            for (int column = 1; column < 10; column++)
            {
                for (int row = 1; row < 10; row++)
                {
                    _sudokoGrid.Cells.Add(new SudokuCell() { ColumnNumber = column, RowNumber = row, Value = 0 });
                }
            }
        }

        public SudokuGrid GetSudokuGrid()
        {
            AddCells();
            return _sudokoGrid;
        }
    }
}
