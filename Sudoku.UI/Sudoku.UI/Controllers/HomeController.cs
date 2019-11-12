using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sudoku.UI.Models;
using Sudoku.UI.Services;

namespace Sudoku.UI.Controllers
{
    public class HomeController : Controller
    {
        private ISudokoGrid _sudokuGrid;

        public HomeController(ISudokoGrid sudokuGrid)
        {
            _sudokuGrid = sudokuGrid;
        }

        public IActionResult Index()
        {
            return View(_sudokuGrid);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult TestForm(List<SudokuCell> cells)
        {
            _sudokuGrid.Cells = cells;
            return View("Index", _sudokuGrid);
        }

        public IActionResult SubmitSudokuPuzzle(List<SudokuCell> cells)
        {
            _sudokuGrid.Cells = cells;
            return View("Index", _sudokuGrid);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
