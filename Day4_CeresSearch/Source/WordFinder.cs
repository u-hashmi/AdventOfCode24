namespace Day4_AdventOfCode24.Source;

public class WordFinder
{
    public static int CountWordOccurrences(string[] grid, string word)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var count = 0;

        var directions = new (int RowDelta, int ColDelta)[]
        {
            (0, 1), // Right
            (0, -1), // Left
            (1, 0), // Down
            (-1, 0), // Up
            (1, 1), // Diagonal down-right
            (1, -1), // Diagonal down-left
            (-1, 1), // Diagonal up-right
            (-1, -1) // Diagonal up-left
        };

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                foreach (var direction in directions)
                {
                    if (FindWord(grid, word, row, col, direction.RowDelta, direction.ColDelta))
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private static bool FindWord(string[] grid, string word, int startRow, int startCol, int rowDelta, int colDelta)
    {
        var rows = grid.Length;
        var cols = grid[0].Length;
        var wordLength = word.Length;

        for (var i = 0; i < wordLength; i++)
        {
            var row = startRow + i * rowDelta;
            var col = startCol + i * colDelta;

            if (row < 0 || row >= rows || col < 0 || col >= cols || grid[row][col] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}