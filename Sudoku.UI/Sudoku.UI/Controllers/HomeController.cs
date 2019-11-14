using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Sudoku.UI.Models;
using Sudoku.UI.Models.Sudoku;
using Sudoku.UI.Services;

namespace Sudoku.UI.Controllers
{
    public class HomeController : Controller
    {
        private IGrid _grid;

        public HomeController(IGrid grid)
        {
            _grid = grid;
            _grid.Boxes = GetBoxes();
        }

        public IActionResult Index()
        {
            return View(_grid);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult SubmitSudokuPuzzle(List<Cell> cells)
        {
            _grid.Cells = cells;
            return View("Index", _grid);
        }

        public List<Cell> GetRelatedCells(Cell cell)
        {
            var relatedCells = new List<Cell>();
            relatedCells.AddRange(_grid.Cells.Where(c => c.Row.Equals(cell.Row)
            || c.Column.Equals(cell.Column)));
            relatedCells.AddRange(GetBoxCells(GetCellBox(cell)));
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
            //foreach (var box in _boxes)
            //{
            //    if (cell.Row >= box.StartRow &&
            //        cell.Row <= box.EndRow &&
            //        cell.Column >= box.StartColumn &&
            //        cell.Column <= box.EndColumn)
            //    {
            //        return box;
            //    }
            //}

            return _grid.Boxes.FirstOrDefault(b =>
                cell.Row >= b.StartRow &&
                cell.Row <= b.EndRow &&
                cell.Column >= b.StartColumn &&
                cell.Column <= b.EndColumn
            );
        }

        private List<Box> GetBoxes()
        {
            var boxes = new List<Box>();
            int i = 0;
            while (i < 9)
            {
                boxes.Add(new Box()
                {
                    StartRow = i,
                    EndRow = i + 2,
                    StartColumn = i,
                    EndColumn = i + 2
                });

                i = i + 3;
            }

            return boxes;
        }

        private void GetCellsInSubGrid(Cell cell)
        {
            var cellsInSubGrid = new List<Cell>();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
