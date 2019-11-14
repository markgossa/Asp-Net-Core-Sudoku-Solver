# Asp-Net-Core-Sudoku-Solver
An ASP.NET Core Web Application to solve Sudoku puzzles

#TODO: Get list of cells in a box:

for (i; i < 9; i+3)
{
box.RowStart = i
box.RowEnd = i
box.ColumnStart = i
box.ColumnEnd = i

//if cell.RowNumber >= box.RowStart .......
}

if(cell.RowNumber <= 2)
{
box.RowStart = 0
box.RowEnd = 2
}
