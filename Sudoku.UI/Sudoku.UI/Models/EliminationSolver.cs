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
        private Grid _initialGrid;
        private Grid _solvedGrid;
        private Attempt _attempt;
        private List<Attempt> _attempts;

        public EliminationSolver()
        {
            _attempts = new List<Attempt>();
            _solvedGrid = new Grid();
        }

        public Grid Solve(Grid grid)
        {
            _initialGrid = grid;

            var i = 0;
            var isSolved = false;
            while (!isSolved)
            {
                Debug.WriteLine($"ATTEMPT {i}: Start");
                _solvedGrid = _initialGrid.Clone() as Grid;
                AddClues();
                _attempt = new Attempt(i);
                SolveCells();
                isSolved = CheckIfSolved();
                _attempts.Add(_attempt);
                Debug.WriteLine($"ATTEMPT {i}: End");
                i++;
            }

            return _solvedGrid;
        }

        private void AddClues()
        {
            _solvedGrid.Cells.FirstOrDefault(c => c.Column == 4 && c.Row == 0).Value = 8;
            _solvedGrid.Cells.FirstOrDefault(c => c.Column == 0 && c.Row == 0).Value = 5;
            //_grid.Cells.FirstOrDefault(c => c.Column == 5 && c.Row == 0).Value = 6;
        }

        private bool CheckIfSolved()
        {
            return !_solvedGrid.Cells.Any(c => c.Value == null);
        }

        private void SolveCells()
        {
            Cell nextCellToSolve;
            while (true)
            {
                PopulateAllCellPossibleValues();
                nextCellToSolve = FindNextCellToSolve();
                if (nextCellToSolve != null)
                {
                    var cellValue = nextCellToSolve.PossibleValues.FirstOrDefault();
                    nextCellToSolve.Value = cellValue;
                    if (nextCellToSolve.PossibleValues.Count > 1)
                    {
                        Debug.WriteLine($"GUESS: Cell in column {nextCellToSolve.Column}, row {nextCellToSolve.Row} could be {String.Join(", ", nextCellToSolve.PossibleValues)} so guessed it is {cellValue}");
                        _attempt.Decisions.Add(new Decision(nextCellToSolve, nextCellToSolve.PossibleValues));
                    }
                    else
                    {
                        Debug.WriteLine($"SOLVED: Cell in column {nextCellToSolve.Column}, row {nextCellToSolve.Row} is {cellValue}");
                    }
                }
                else
                {
                    break;
                }
            };
        }

        private Cell FindNextCellToSolve()
        {
            return _solvedGrid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 1)
                ?? _solvedGrid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 2);
        }

        private void PopulateAllCellPossibleValues()
        {
            _solvedGrid.Cells.Where(c => !c.Value.HasValue).ToList()
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
            relatedCells.AddRange(_solvedGrid.Cells.Where(c => c.Row.Equals(cell.Row) && !c.Column.Equals(cell.Column)));
            relatedCells.AddRange(_solvedGrid.Cells.Where(c => c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));
            relatedCells.AddRange(GetBoxRelatedCells(GetCellBox(cell))
                .Where(c => !c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));

            return relatedCells.Where(c => c.Value.HasValue).ToList();
        }

        private List<Cell> GetBoxRelatedCells(Box box)
        {
            return _solvedGrid.Cells.Where(c =>
                c.Row >= box.StartRow &&
                c.Row <= box.EndRow &&
                c.Column >= box.StartColumn &&
                c.Column <= box.EndColumn
            ).ToList();
        }

        private Box GetCellBox(Cell cell)
        {
            return _solvedGrid.Boxes.FirstOrDefault(b =>
                cell.Row >= b.StartRow &&
                cell.Row <= b.EndRow &&
                cell.Column >= b.StartColumn &&
                cell.Column <= b.EndColumn
            );
        }
    }
}
