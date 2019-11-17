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
        private readonly IPuzzle _samplePuzzle;
        private readonly Grid _grid;

        public GridBuilder(IPuzzle samplePuzzle)
        {
            _samplePuzzle = samplePuzzle;
            _grid = new Grid();
            _grid.Cells = new List<Cell>();
            _grid.Boxes = new List<Box>();
            AddCells();
            AddBoxes();
            AddSamplePuzzle();
        }

        private void AddCells()
        {
            for (int column = 0; column < 9; column++)
            {
                for (int row = 0; row < 9; row++)
                {
                    _grid.Cells.Add(new Cell() {
                        Column = column,
                        Row = row,
                        PossibleValues = new List<int>()
                    });
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

        private void AddSamplePuzzle()
        {
            var puzzleCells = _samplePuzzle.GetPuzzle();
            if (puzzleCells.Any())
            {
                for (int i = 0; i < puzzleCells.Count; i++)
                {
                    _grid.Cells[i].Value = puzzleCells[i];
                }
            }
        }
    }
}
