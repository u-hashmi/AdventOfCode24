using Day4_AdventOfCode24.Source;

namespace Day4_AdventOfCode24;

public static class Program
{
    public static void Main(string[] args)
    {
        const string filePath = "Day4_CeresSearch_Part2_Input.txt";
        const string part2FilePath = "Day4_CeresSearch_Part2_Input.txt";

        var grid1 = ReadGridFromFile(filePath);
        var grid2 = ReadGridFromFile(part2FilePath);

        if (grid1.Length == 0 || grid2.Length == 0)
        {
            Console.WriteLine("One or more grids are empty or invalid.");
            return;
        }

        // Example for Part 2
        var xmasDiagonalPatternCount = XmasPatternFinder.CountXmasPatterns(grid2);
        var xmasPatternCount = WordFinder.CountWordOccurrences(grid1, "XMAS");
        Console.WriteLine($"Xmas pattern count: {xmasPatternCount}");
        Console.WriteLine($"Xmas diagonal pattern count: {xmasDiagonalPatternCount}");
    }
    
    public static string[] ReadGridFromFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            return Array.Empty<string>();
        }
    }
}