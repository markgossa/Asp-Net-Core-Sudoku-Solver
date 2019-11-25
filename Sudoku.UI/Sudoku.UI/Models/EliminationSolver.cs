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
        private Attempt _attempt;
        private readonly List<Attempt> _attempts;
        private const int _maxDecisionCount = 10;

        public EliminationSolver()
        {
            _attempts = new List<Attempt>();
        }

        public async Task<Grid> Solve(Grid gridToSolve)
        {
            //var tasks = new List<Task<(Grid, bool)>>();
            //List<int> attemptModifier = null;
            //for (int attemptNumber = 0; attemptNumber < Math.Pow(2, _maxDecisionCount); attemptNumber++)
            //{
            //    attemptModifier = CreateNewAttemptModifier(attemptNumber);
            //    tasks.Add(CreateNewAtempt(attemptNumber, gridToSolve, attemptModifier));
            //}

            //var result = await Task.WhenAll<(Grid, bool)>(tasks);

            //var solution = result.FirstOrDefault(r => r.Item2).Item1;

            var results = new List<(Grid, bool)>();
            List<int> attemptModifier = null;
            for (int attemptNumber = 0; attemptNumber < Math.Pow(2, _maxDecisionCount); attemptNumber++)
            {
                attemptModifier = CreateNewAttemptModifier(attemptNumber);
                var task = CreateNewAtempt(attemptNumber, gridToSolve, attemptModifier);
                await task;
                results.Add(task.Result);
            }

            var solution = results.FirstOrDefault(r => r.Item2).Item1;

            return solution;
        }

        private async Task<(Grid, bool)> CreateNewAtempt(int attemptNumber, Grid gridToSolve, List<int> attemptModifier)
        {
            Grid grid = null;
            Debug.WriteLine($"ATTEMPT {attemptNumber}: Start");
            Debug.WriteLine($"ATTEMPT {attemptNumber}: Attempt modifier {string.Join(", ", attemptModifier)}");
            grid = gridToSolve.Clone() as Grid;
            _attempt = new Attempt(attemptNumber);

            await Task.Run(() =>
            {
                grid = SolveCells(grid, attemptModifier);
                _attempts.Add(_attempt);
            });

            var isSolved = CheckIfSolved(grid);
            if (isSolved)
            {
                Debug.WriteLine($"ATTEMPT {attemptNumber}: SOLVED!");
            }

            Debug.WriteLine($"ATTEMPT {attemptNumber}: End");

            return (grid, isSolved);
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

        private bool CheckIfSolved(Grid grid)
        {
            return !grid.Cells.Any(c => c.Value.Equals(null));
        }

        private Grid SolveCells(Grid grid, List<int> attemptModifier = null)
        {
            Cell nextCellToSolve;
            while (true)
            {
                PopulateAllCellPossibleValues(grid);
                nextCellToSolve = FindNextCellToSolve(grid);
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

            return grid;
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

        private Cell FindNextCellToSolve(Grid grid)
        {
            return grid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 1)
                ?? grid.Cells.FirstOrDefault(c => !c.Value.HasValue && c.PossibleValues != null && c.PossibleValues.Count == 2);
        }

        private void PopulateAllCellPossibleValues(Grid grid)
        {
            grid.Cells.Where(c => !c.Value.HasValue).ToList()
                .ForEach(c => c.PossibleValues = GetCellPossibleValues(c, grid));
        }

        private List<int> GetCellPossibleValues(Cell cell, Grid grid)
        {
            var relatedCellUniqueValues = GetRelatedSolvedCells(cell, grid)
                .Select(c => c.Value ?? default)
                .Distinct();

            return Enumerable.Range(1, 9).Except(relatedCellUniqueValues).ToList();
        }

        private List<Cell> GetRelatedSolvedCells(Cell cell, Grid grid)
        {
            var relatedCells = new List<Cell>();
            relatedCells.AddRange(grid.Cells.Where(c => c.Row.Equals(cell.Row) && !c.Column.Equals(cell.Column)));
            relatedCells.AddRange(grid.Cells.Where(c => c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));
            relatedCells.AddRange(GetBoxRelatedCells(GetCellBox(cell, grid), grid)
                .Where(c => !c.Column.Equals(cell.Column) && !c.Row.Equals(cell.Row)));

            return relatedCells.Where(c => c.Value.HasValue).ToList();
        }

        private List<Cell> GetBoxRelatedCells(Box box, Grid grid)
        {
            return grid.Cells.Where(c =>
                c.Row >= box.StartRow &&
                c.Row <= box.EndRow &&
                c.Column >= box.StartColumn &&
                c.Column <= box.EndColumn
            ).ToList();
        }

        private Box GetCellBox(Cell cell, Grid grid)
        {
            return grid.Boxes.FirstOrDefault(b =>
                cell.Row >= b.StartRow &&
                cell.Row <= b.EndRow &&
                cell.Column >= b.StartColumn &&
                cell.Column <= b.EndColumn
            );
        }
    }
}
