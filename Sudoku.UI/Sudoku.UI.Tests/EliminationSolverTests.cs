using Sudoku.UI.Models;
using Sudoku.UI.Tests.Models;
using Sudoku.UI.Tests.Models.Puzzles;
using Xunit;
using Sudoku.UI.Models.Sudoku;
using System.Collections.Generic;
using System.Linq;
using System;
using Sudoku.UI.Tests.Services;

namespace Sudoku.UI.Tests.Tests
{
    public class EliminationSolverTests
    {
        private readonly EliminationSolver _sut;
        private Grid _solvedGrid;

        public EliminationSolverTests()
        {
            _sut = new EliminationSolver();
        }

        [Fact]
        public void SolveEasyPuzzle1()
        {
            var puzzle = new EasyPuzzle1();
            AssertPuzzleSolved(puzzle);
        }

        [Fact]
        public void SolveEasyPuzzle2()
        {
            var puzzle = new EasyPuzzle2();
            AssertPuzzleSolved(puzzle);
        }

        [Fact]
        public void SolveEasyPuzzle3()
        {
            var puzzle = new EasyPuzzle3();
            AssertPuzzleSolved(puzzle);
        }

        [Fact]
        public void SolveExpertPuzzle1()
        {
            var puzzle = new ExpertPuzzle1();
            AssertPuzzleSolved(puzzle);
        }

        [Fact]
        public void SolveHardPuzzle1()
        {
            var puzzle = new HardPuzzle1();
            AssertPuzzleSolved(puzzle);
        }

        [Fact]
        public void SolveHardPuzzle2()
        {
            var puzzle = new HardPuzzle2();
            AssertPuzzleSolved(puzzle);
        }
        [Fact]
        public void SolveHardPuzzle3()
        {
            var puzzle = new HardPuzzle3();
            AssertPuzzleSolved(puzzle);
        }

        [Fact(Skip = "Test takes too long")]
        public void SolveWorldsHardestPuzzle()
        {
            var puzzle = new WorldsHardestPuzzle();
            AssertPuzzleSolved(puzzle);
        }

        private void AssertPuzzleSolved(IPuzzle puzzle)
        {
            var grid = new GridBuilder(puzzle).GetSudokuGrid();
            _solvedGrid = _sut.SolveAsync(grid).Result;

            Assert.True(CheckPuzzleIsSolved());
        }

        private bool CheckPuzzleIsSolved()
        {
            return CheckBoxesSolved() && CheckRowsAndColumnsSolved();
        }

        private bool CheckRowsAndColumnsSolved()
        {
            for (int i = 0; i < 9; i++)
            {
                if (!CheckRowSolved(i) && !CheckColumnSolved(i))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckColumnSolved(int columnNumber)
        {
            var cellValues = _solvedGrid.Cells.Where(c => c.Column.Equals(columnNumber)).Select(c => (int)c.Value);

            return CheckRelatedCellsSolved(cellValues);
        }

        private bool CheckRowSolved(int rowNumber)
        {
            var cellValues = _solvedGrid.Cells.Where(c => c.Row.Equals(rowNumber)).Select(c => (int)c.Value);

            return CheckRelatedCellsSolved(cellValues);
        }

        private bool CheckBoxesSolved()
        {
            foreach(var box in _solvedGrid.Boxes)
            {
                if (!CheckBoxIsSolved(box))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckBoxIsSolved(Box box)
        {
            var cellValues = GetBoxRelatedCells(box).Select(c => c.Value ?? default);

            return CheckRelatedCellsSolved(cellValues);
        }

        private bool CheckRelatedCellsSolved(IEnumerable<int> cellValues) => Enumerable.Range(1, 9).Except(cellValues).Count().Equals(0);

        private List<Cell> GetBoxRelatedCells(Box box)
        {
            return _solvedGrid.Cells.Where(c =>
                c.Row >= box.StartRow &&
                c.Row <= box.EndRow &&
                c.Column >= box.StartColumn &&
                c.Column <= box.EndColumn
            ).ToList();
        }
    }
}
