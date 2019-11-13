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
        private IGrid _grid;

        public HomeController(IGrid grid)
        {
            _grid = grid;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
