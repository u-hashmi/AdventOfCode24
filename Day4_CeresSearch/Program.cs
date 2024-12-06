using Day4_AdventOfCode24.Source;
using Helpers;

namespace Day4_AdventOfCode24;

public static class Program
{
    public static void Main(string[] args)
    {
        var part1FilePath = new FilePathRecord("Day4_CeresSearch_Part2_Input.txt");
        var part2FilePath = new FilePathRecord("Day4_CeresSearch_Part2_Input.txt");

        var grid1 = part1FilePath.ReadFromFile();
        var grid2 = part2FilePath.ReadFromFile();

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
}