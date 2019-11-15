using Sudoku.UI.Models;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku
{
    public class GridBuilder : IGridBuilder
    {
        private readonly Grid _sudokoGrid;

        public GridBuilder()
        {
            _sudokoGrid = new Grid();
            _sudokoGrid.Cells = new List<Cell>();
        }

        private void AddCells()
        {
            for (int column = 1; column < 10; column++)
            {
                for (int row = 1; row < 10; row++)
                {
                    _sudokoGrid.Cells.Add(new Cell() { Column = column, Row = row, Value = 0 });
                }
            }
        }

        public Grid GetSudokuGrid()
        {
            AddCells();
            return _sudokoGrid;
        }
    }
}
