using Helpers;

namespace Day6_GuardGallivant.Classes;

public static class Utils
{
    public static void PrintCharMap(this Map map)
    {
        foreach (var mapLine in map.GetMap())
        {
            foreach (var character in mapLine)
                Console.Write(character);

            Console.WriteLine();
        }
    }

    public static Map ConvertToCharMap(string[] recordData) =>
        Map.Create(recordData.Select(record => record.ToCharArray().ToList()).ToList());

    public static List<Position> GetObstaclePositions(this Map map) =>
        map.GetMap()
            .Select((row, rowIndex) => new { row, rowIndex })
            .Where(r => r.row.Contains('#'))
            .Select((r) => Position.CreatePositions(r.rowIndex, r.row.IndexOf('#')))
            .ToList();

    public static Position GetSoldierPosition(this Map map) =>
        map.GetMap()
            .Select((row, rowIndex) => new { row, rowIndex })
            .Where(r => r.row.Contains('^'))
            .Select(r => Position.CreatePositions(r.rowIndex, r.row.IndexOf('^')))
            .FirstOrDefault()!;


    public static void PrintObstaclesPosition(this List<Position> positionList)
    {
        foreach (var pos in positionList)
            Console.WriteLine($"Line: {pos.Line} | Index: {pos.Index}");
    }

    public static void PrintSoliderPosition(this Position soldierPosition) =>
        Console.WriteLine($"Line: {soldierPosition.Line} | Index: {soldierPosition.Index}");
    
}