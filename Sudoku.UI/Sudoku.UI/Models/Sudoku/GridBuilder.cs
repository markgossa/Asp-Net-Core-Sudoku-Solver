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
            AddSamplePuzzle();
        }

        public Grid GetSudokuGrid()
        {
            return _grid;
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
