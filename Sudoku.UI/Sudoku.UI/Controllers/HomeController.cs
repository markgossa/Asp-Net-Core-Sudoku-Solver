using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sudoku.UI.Models;
using Sudoku.UI.Models.Sudoku;
using Sudoku.UI.Services;

namespace Sudoku.UI.Controllers
{
    public class HomeController : Controller
    {
        private Grid _grid;
        private ISolver _solver;

        public HomeController(Grid grid, ISolver solver)
        {
            _grid = grid;
            _solver = solver;
        }

        public IActionResult Index(List<Cell> cells)
        {
            return View("Index", _grid.Cells);
        }

        public IActionResult Solve(List<Cell> cells)
        {
            _grid.Cells = cells;
            var solvedGrid = _solver.SolveAsync(_grid);

            return View(solvedGrid.Result.Cells);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
