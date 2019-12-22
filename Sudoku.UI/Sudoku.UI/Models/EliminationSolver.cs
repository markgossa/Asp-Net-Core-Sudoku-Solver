using Sudoku.UI.Models.Sudoku;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models
{
    public class EliminationSolver : ISolver
    {
        private const int _maxDecisionCount = 14;
        private Grid gridToSolve;
        private Grid gridSolution;
        private int attemptNumber;

        public async Task<Grid> SolveAsync(Grid grid)
        {
            gridToSolve = grid;
            await foreach (var result in RunAttemptsAsync())
            {
                if (result)
                {
                    return gridSolution;
                }
            }

            return new Grid();
        }

        public async IAsyncEnumerable<bool> RunAttemptsAsync()
        {
            for (attemptNumber = 0; attemptNumber < Math.Pow(2, _maxDecisionCount); attemptNumber++)
            {
                var attemptModifier = CreateNewAttemptModifier();
                yield return await CreateNewAttemptAsync(attemptModifier);
            }
        }

        private async Task<bool> CreateNewAttemptAsync(List<int> attemptModifier)
        {
            Log("Start");
            Log($"Attempt modifier { string.Join(", ", attemptModifier)}");
            gridSolution = gridToSolve.Clone() as Grid;

            await Task.Run(() =>
            {
                gridSolution = SolveCells(new Attempt(attemptNumber), attemptModifier);
            });

            var isSolved = CheckIfSolved();
            if (isSolved)
            {
                Log("SOLVED!");
            }

            Log("End");
            Log($"Cells: {String.Join(", ", gridSolution.Cells.Select(c => c.Value).ToList())}");

            return isSolved;
        }

        private List<int> CreateNewAttemptModifier()
        {
            var nextAttemptModifier = Convert.ToString(attemptNumber + 1, 2).PadLeft(_maxDecisionCount, '0');

            var list = new List<int>();
            for (int i = nextAttemptModifier.Length - 1; i >= 0; i--)
            {
                list.Add(int.Parse(nextAttemptModifier[i].ToString()));
            }
            
            return list;
        }

        private bool CheckIfSolved()
        {
            return !gridSolution.Cells.Any(c => c.Value.Equals(null));
        }

        private Grid SolveCells(Attempt attempt, List<int> attemptModifier = null)
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
                            ProcessCellDecision(nextCellToSolve, attemptModifier, attempt);
                        }
                        catch
                        {
                            Log("Abort. Too many decisions required.");
                            break;
                        }
                    }
                    else
                    {
                        var cellValue = nextCellToSolve.PossibleValues.FirstOrDefault();
                        nextCellToSolve.Value = cellValue;
                    }
                }
                else
                {
                    break;
                }
            };

            return gridSolution;
        }

        private void ProcessCellDecision(Cell cell, List<int> attemptModifier, Attempt attempt)
        {
            var cellDecisionNumber = attempt?.Decisions.Count ?? 0;

            if (cellDecisionNumber == _maxDecisionCount)
            {
                throw new TooManyDecisionsException();
            }

            var cellPossibleValueIndex = attemptModifier?[cellDecisionNumber] ?? 0; 
            cell.Value = cell.PossibleValues[cellPossibleValueIndex];

            attempt.Decisions.Add(new Decision(cell));
            Log($"GUESS Cell in column {cell.Column}, row {cell.Row} could be {String.Join(", or ", cell.PossibleValues)} so guessed it is {cell.Value}");
        }

        private Cell FindNextCellToSolve()
        {
            return gridSolution.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 1)
                ?? gridSolution.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 2);
        }

        private void PopulateAllCellPossibleValues()
        {
            gridSolution.Cells.Where(c => !c.Value.HasValue).ToList()
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
            relatedCells.AddRange(gridSolution.Cells.Where(c => c.Row.Equals(cell.Row) && !c.Column.Equals(cell.Column)));
            relatedCells.AddRange(gridSolution.Cells.Where(c => c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));
            relatedCells.AddRange(GetBoxRelatedCells(GetCellBox(cell))
                .Where(c => !c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));

            return relatedCells.Where(c => c.Value.HasValue).ToList();
        }

        private List<Cell> GetBoxRelatedCells(Box box)
        {
            return gridSolution.Cells.Where(c =>
                c.Row >= box.StartRow &&
                c.Row <= box.EndRow &&
                c.Column >= box.StartColumn &&
                c.Column <= box.EndColumn
            ).ToList();
        }

        private Box GetCellBox(Cell cell)
        {
            return gridSolution.Boxes.FirstOrDefault(b =>
                cell.Row >= b.StartRow &&
                cell.Row <= b.EndRow &&
                cell.Column >= b.StartColumn &&
                cell.Column <= b.EndColumn
            );
        }

        [Conditional("LOG")]
        private void Log(string message)
        {
            Debug.WriteLine($"ATTEMPT {attemptNumber}: {message}");
        }
    }
}
