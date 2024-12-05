namespace Day4_AdventOfCode24.Source;

public static class DirectionFinder
{
    public static bool NorthWestElement(this string[] grid, char element, int row, int column) =>
        grid[row - 1][column - 1] == element;

    public static bool NorthEastElement(this string[] grid, char element, int row, int column) =>
        grid[row - 1][column + 1] == element;

    public static bool SouthWestElement(this string[] grid, char element, int row, int column) =>
        grid[row + 1][column - 1] == element;

    public static bool SouthEastElement(this string[] grid, char element, int row, int column) =>
        grid[row + 1][column + 1] == element;
    
    public static bool IsCenterElement(this string[] grid, char element, int row, int column) =>
        grid[row][column] == element;
    
    public static bool IsLeftElement(this string[] grid, char element, int row, int column) => 
     grid[row][column - 1] == element;
    
    public static bool IsRightElement(this string[] grid, char element, int row, int column) => 
        grid[row][column + 1] == element; 
    
    public static bool IsTopElement(this string[] grid, char element, int row, int column) => 
        grid[row - 1][column] == element; 
    
    public static bool IsBottomElement(this string[] grid, char element, int row, int column) => 
        grid[row + 1][column] == element; 
}