using System.Diagnostics.CodeAnalysis;

namespace Day6_GuardGallivant.Classes;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class Position
{
    public int Line { get; set; }
    public int Index { get; set; }

    private Position(int line, int index)
    {
        Line = line;
        Index = index;
    }

    public static Position CreatePositions(int yAxis, int xAxis)
    {
        return new Position(yAxis, xAxis);
    }
    
    public void UpdateXAxis(int x) => Index = x;
    public void UpdateYAxis(int y) => Line = y;
}