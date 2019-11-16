using Sudoku.UI.Models.Sudoku;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class EliminationSolver : ISolver
    {
        private IGrid _grid;

        public IGrid Solve(IGrid grid)
        {
            _grid = grid;
            _grid.Cells[5].Value = 9;

            return _grid;
        }

        private List<Cell> GetRelatedCells(Cell cell)
        {
            var relatedCells = new List<Cell>();
            relatedCells.AddRange(_grid.Cells.Where(c => c.Row.Equals(cell.Row)
            || c.Column.Equals(cell.Column)));
            relatedCells.AddRange(GetBoxCells(GetCellBox(cell)));
            relatedCells.Remove(cell);

            return relatedCells;
        }

        private List<Cell> GetBoxCells(Box box)
        {
            return _grid.Cells.Where(c =>
                c.Row >= box.StartRow &&
                c.Row <= box.EndRow &&
                c.Column >= box.StartColumn &&
                c.Column <= box.EndColumn
            ).ToList();
        }

        private Box GetCellBox(Cell cell)
        {
            return _grid.Boxes.FirstOrDefault(b =>
                cell.Row >= b.StartRow &&
                cell.Row <= b.EndRow &&
                cell.Column >= b.StartColumn &&
                cell.Column <= b.EndColumn
            );
        }
    }
}
