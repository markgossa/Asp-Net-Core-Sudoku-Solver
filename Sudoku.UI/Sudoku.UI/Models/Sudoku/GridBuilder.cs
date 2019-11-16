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
        private readonly Grid _grid;

        public GridBuilder()
        {
            _grid = new Grid();
            _grid.Cells = new List<Cell>();
            _grid.Boxes = new List<Box>();
            AddCells();
            AddBoxes();
        }

        private void AddCells()
        {
            for (int column = 1; column < 10; column++)
            {
                for (int row = 1; row < 10; row++)
                {
                    _grid.Cells.Add(new Cell() { Column = column, Row = row});
                }
            }
        }

        public Grid GetSudokuGrid()
        {
            

            return _grid;
        }

        private void AddBoxes()
        {
            _grid.Boxes = new List<Box>();
            int row = 0;
            while (row < 9)
            {
                int column = 0;
                while (column < 9)
                {
                    _grid.Boxes.Add(new Box()
                    {
                        StartRow = row,
                        EndRow = row + 2,
                        StartColumn = column,
                        EndColumn = column + 2
                    });

                    column += 3;
                }

                row += 3;
            }
        }
    }
}
