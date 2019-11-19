using Microsoft.AspNetCore.Mvc;
using Sudoku.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.UI.Models.Sudoku
{
    public class Grid : IGrid
    {
        [BindProperty]
        public List<Cell> Cells { get; set; }

        public List<Box> Boxes { get; set; }

        public Grid()
        {
            Cells = new List<Cell>();
            Boxes = new List<Box>();
            AddBoxes();
            AddCells();
        }

        private void AddBoxes()
        {
            int row = 0;
            while (row < 9)
            {
                int column = 0;
                while (column < 9)
                {
                    Boxes.Add(new Box()
                    {
                        StartRow = row,
                        EndRow = row + 2,
                        StartColumn = column,
                        EndColumn = column + 2
                    });

                    column += 3;
                }

                row += 3;
            }
        }

        private void AddCells()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    Cells.Add(new Cell()
                    {
                        Column = column,
                        Row = row,
                        PossibleValues = new List<int>()
                    });
                }
            }
        }
    }
}
