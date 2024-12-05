using System.Diagnostics.CodeAnalysis;

namespace Day4_AdventOfCode24.Source;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class XmasPatternFinder
{
    public static int CountXmasPatterns(string[] grid)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var count = 0;

        // Search for patterns, avoiding borders
        for (var row = 1; row < rows - 1; row++)
        {
            for (var col = 1; col < cols - 1; col++)
            {
                if (IsXmasPattern(grid, row, col)) 
                    count++;
            }
        }

        return count;
    }


    private static bool IsXmasPattern(string[] grid, int row, int col)
    {
        // Ensure we stay within the bounds of the grid
        const char letterM = 'M';
        const char letterS = 'S';
        const char letterA = 'A';

        if (IsPointerOnEdge(row, col, grid.Length, grid[0].Length))
            return false;
            
        // M.S
        //  A
        // M.S
        var isMSAMS = grid.NorthWestElement(letterM, row, col) &&
                      grid.NorthEastElement(letterS, row, col) &&
                      grid.IsCenterElement(letterA, row, col) &&
                      grid.SouthWestElement(letterM, row, col) &&
                      grid.SouthEastElement(letterS, row, col);

        // Top-right to bottom-left diagonal
        // S.M
        //  A
        // S.M
        var isSMASM = grid.NorthWestElement(letterS, row, col) &&
                      grid.NorthEastElement(letterM, row, col) &&
                      grid.IsCenterElement(letterA, row, col) &&
                      grid.SouthWestElement(letterS, row, col) &&
                      grid.SouthEastElement(letterM, row, col);

        // S.S
        //  A
        // M.M
        var isSSAMM = grid.NorthWestElement(letterS, row, col) &&
                      grid.NorthEastElement(letterS, row, col) &&
                      grid.IsCenterElement(letterA, row, col) &&
                      grid.SouthWestElement(letterM, row, col) &&
                      grid.SouthEastElement(letterM, row, col);

        // S.S
        //  A
        // M.M
        var isMMASS = grid.NorthWestElement(letterM, row, col) &&
                      grid.NorthEastElement(letterM, row, col) &&
                      grid.IsCenterElement(letterA, row, col) &&
                      grid.SouthWestElement(letterS, row, col) &&
                      grid.SouthEastElement(letterS, row, col);

        // Return true if either diagonal forms a valid "X-MAS" pattern
        return isMSAMS || isSMASM || isSSAMM || isMMASS;
    }

    private static bool IsPointerOnEdge(int row, int col, int rowLength, int colLength) =>
        row - 1 < 0 || row + 1 >= rowLength || col - 1 < 0 || col + 1 >= colLength;
}