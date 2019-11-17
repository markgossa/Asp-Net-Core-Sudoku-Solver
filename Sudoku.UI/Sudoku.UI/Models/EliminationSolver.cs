using Sudoku.UI.Models.Sudoku;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sudoku.UI.Models
{
    public class EliminationSolver : ISolver
    {
        private IGrid _grid;

        public EliminationSolver(IGrid grid)
        {
            _grid = grid;
        }

        public IGrid Solve()
        {
            Cell nextSolvedCell;
            while (true)
            {
                PopulateAllCellPossibleValues();
                nextSolvedCell = FindNextCellToSolve();
                if (nextSolvedCell != null)
                {
                    var cellValue = nextSolvedCell.PossibleValues.FirstOrDefault();
                    nextSolvedCell.Value = cellValue;
                    if (nextSolvedCell.PossibleValues.Count > 1)
                    {
                        Debug.WriteLine($"GUESS: Cell in column {nextSolvedCell.Column}, row {nextSolvedCell.Row} could be {String.Join(", ",nextSolvedCell.PossibleValues)} so guessed it is {cellValue}");
                    }
                    else
                    {
                        Debug.WriteLine($"SOLVED: Cell in column {nextSolvedCell.Column}, row {nextSolvedCell.Row} is {cellValue}");
                    }
                }
                else
                {
                    break;
                }
            };

            return _grid;
        }

        private Cell FindNextCellToSolve()
        {
            return _grid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 1)
                ?? _grid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 2);
        }

        private void PopulateAllCellPossibleValues()
        {
            _grid.Cells.Where(c => !c.Value.HasValue).ToList()
                .ForEach(c => c.PossibleValues = GetCellPossibleValues(c));
        }

        private List<int> GetCellPossibleValues(Cell cell)
        {
            var relatedCellUniqueValues = GetRelatedSolvedCells(cell)
                .Select(c => c.Value ?? default(int))
                .Distinct();

            return Enumerable.Range(1, 9).Except(relatedCellUniqueValues).ToList();
        }

        private List<Cell> GetRelatedSolvedCells(Cell cell)
        {
            var relatedCells = new List<Cell>();
            relatedCells.AddRange(_grid.Cells.Where(c => c.Row.Equals(cell.Row) && !c.Column.Equals(cell.Column)));
            relatedCells.AddRange(_grid.Cells.Where(c => c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));
            relatedCells.AddRange(GetBoxRelatedCells(GetCellBox(cell))
                .Where(c => !c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));

            return relatedCells.Where(c => c.Value.HasValue).ToList();
        }

        private List<Cell> GetBoxRelatedCells(Box box)
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
