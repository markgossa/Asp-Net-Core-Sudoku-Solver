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
        private readonly List<Attempt> _attempts;
        private const int _maxDecisionCount = 5;

        public EliminationSolver()
        {
            _attempts = new List<Attempt>();
            _solvedGrid = new Grid();
        }

        public Grid Solve(Grid grid)
        {
            _initialGrid = grid;
            var attemptNumber = 0;
            var isSolved = false;
            List<int> attemptModifier = null;
            while (!isSolved)
            {
                Debug.WriteLine($"ATTEMPT {attemptNumber}: Start");
                _solvedGrid = _initialGrid.Clone() as Grid;
                AddClues();
                _attempt = new Attempt(attemptNumber);
                SolveCells(attemptModifier);

                isSolved = CheckIfSolved();
                _attempts.Add(_attempt);
                if(!isSolved)
                {
                    attemptModifier = CreateNewAttemptModifier(attemptNumber);
                }
                
                Debug.WriteLine($"ATTEMPT {attemptNumber}: End");
                attemptNumber++;
            }

            return _solvedGrid;
        }

        private List<int> CreateNewAttemptModifier(int attemptNumber)
        {
            var nextAttemptModifier = Convert.ToString(attemptNumber + 1, 2).PadLeft(_maxDecisionCount, '0'); // 001

            var list = new List<int>();
            for (int i = 0; i < nextAttemptModifier.Length; i++)
            {
                list.Add(int.Parse(nextAttemptModifier[i].ToString()));
            }

            return list;
        }

        private void AddClues()
        {
            //_solvedGrid.Cells.FirstOrDefault(c => c.Column == 4 && c.Row == 0).Value = 8;
            //_solvedGrid.Cells.FirstOrDefault(c => c.Column == 0 && c.Row == 0).Value = 5;
            //_solvedGrid.Cells.FirstOrDefault(c => c.Column == 5 && c.Row == 0).Value = 6;
        }

        private bool CheckIfSolved()
        {
            return !_solvedGrid.Cells.Any(c => c.Value.Equals(null));
        }

        private void SolveCells(List<int> attemptModifier = null)
        {
            Cell nextCellToSolve;
            while (true)
            {
                PopulateAllCellPossibleValues();
                nextCellToSolve = FindNextCellToSolve();
                if (nextCellToSolve != null)
                {
                    if (nextCellToSolve.PossibleValues.Count > 1)
                    {
                        try
                        {
                            ProcessCellDecision(nextCellToSolve, attemptModifier);
                        }
                        catch
                        {
                            Debug.WriteLine("ATTEMPT: Abort. Too many decisions required.");
                            break;
                        }
                    }
                    else
                    {
                        var cellValue = nextCellToSolve.PossibleValues.FirstOrDefault();
                        nextCellToSolve.Value = cellValue;
                        //Debug.WriteLine($"SOLVED: Cell in column {nextCellToSolve.Column}, row {nextCellToSolve.Row} is {cellValue}");
                    }
                }
                else
                {
                    break;
                }
            };
        }

        private void ProcessCellDecision(Cell cell, List<int> attemptModifier)
        {
            var cellDecisionNumber = _attempt?.Decisions.Count ?? 0;

            if (cellDecisionNumber == _maxDecisionCount)
            {
                throw new TooManyDecisionsException();
            }

            var cellPossibleValueIndex = attemptModifier?[cellDecisionNumber] ?? 0; 
            cell.Value = cell.PossibleValues[cellPossibleValueIndex];

            _attempt.Decisions.Add(new Decision(cell));
            Debug.WriteLine($"GUESS: Cell in column {cell.Column}, row {cell.Row} could be {String.Join(", or ", cell.PossibleValues)} so guessed it is {cell.Value}");
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
                .Select(c => c.Value ?? default)
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
