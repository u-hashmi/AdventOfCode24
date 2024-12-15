using Day6_GuardGallivant.Classes;
using Helpers;

namespace Day6_GuardGallivant;

public static class Program
{
    public static void Main()
    {
        var exampleInputFile = new FilePathRecord("./Inputs/Day6_GuardGallivant_Second_Input.txt");
        FindGallivantSteps(exampleInputFile);
    }

    private static void FindGallivantSteps(FilePathRecord record)
    {
        var recordData = record.ReadFromFile();
        var mapObj = Utils.ConvertToCharMap(recordData);
        mapObj.PrintCharMap();
        Console.WriteLine();

        MoveAndFindSteps(mapObj);
        MoveAndPlaceObstacles(mapObj, recordData);

        Console.WriteLine($"Obstacle Count: {mapObj.MovingSteps}");
        Console.WriteLine($"Hurdles Count: {mapObj.Hurdles.Count}");
    }

    private static void MoveAndFindSteps(Map mapObj)
    {
        var soldier = mapObj.GetSoldierPosition();
        while (!IsAtBoundary(soldier, mapObj))
        {
            if (mapObj.CurrentDirection is 'U' or 'D')
            {
                MoveVertically(mapObj, soldier);
            }
            else
            {
                MoveHorizontally(mapObj, soldier);
            }

            mapObj.CurrentDirection = GetNextDirection(mapObj.CurrentDirection);
        }
    }

    private static void MoveAndPlaceObstacles(Map markedMap, string[] recordData)
    {
        var soldier = Utils.ConvertToCharMap(recordData).GetSoldierPosition();
        while (!IsAtBoundary(soldier, markedMap))
        {
            if (markedMap.CurrentDirection is 'U' or 'D')
            {
                MoveVertically(markedMap, soldier, placeObstacles: true);
            }
            else
            {
                MoveHorizontally(markedMap, soldier, placeObstacles: true);
            }

            markedMap.CurrentDirection = GetNextDirection(markedMap.CurrentDirection);
        }
    }

    private static char GetNextDirection(char currentDirection) =>
        currentDirection switch
        {
            'U' => 'R',
            'R' => 'D',
            'D' => 'L',
            'L' => 'U',
            _ => throw new InvalidOperationException("Invalid direction")
        };

    private static void MoveVertically(Map mapObj, Position soldier, bool placeObstacles = false)
    {
        while (true)
        {
            var nextPos = mapObj.CurrentDirection == 'U' ? soldier.Line - 1 : soldier.Line + 1;
            if (IsBlocked(mapObj, soldier.Index, nextPos)) break;

            if (mapObj.MapData[nextPos][soldier.Index] != 'X')
            {
                mapObj.MapData[nextPos][soldier.Index] = 'X';
                mapObj.MovingSteps++;
            }

            var foundLoopHoles = mapObj.CurrentDirection == 'U'
                ? CheckRightMovingUp(soldier, mapObj)
                : CheckLeftMovingDown(soldier, mapObj);

            if (foundLoopHoles && placeObstacles)
            {
                var pos = Position.CreatePositions(
                    mapObj.CurrentDirection == 'U' ? soldier.Line - 1 : soldier.Line + 1, soldier.Index);
                mapObj.Hurdles.Add(pos);
            }

            soldier.UpdateYAxis(nextPos);
        }
    }

    private static void MoveHorizontally(Map mapObj, Position soldier, bool placeObstacles = false)
    {
        while (true)
        {
            var nextPos = mapObj.CurrentDirection == 'L' ? soldier.Index - 1 : soldier.Index + 1;
            if (IsBlocked(mapObj, nextPos, soldier.Line)) break;

            if (mapObj.MapData[soldier.Line][nextPos] != 'X')
            {
                mapObj.MovingSteps++;
                mapObj.MapData[soldier.Line][nextPos] = 'X';
            }

            var foundLoopHoles = mapObj.CurrentDirection == 'L'
                ? CheckTopMovingLeft(soldier, mapObj)
                : CheckBottomMovingRight(soldier, mapObj);

            if (foundLoopHoles && placeObstacles)
            {
                var pos = Position.CreatePositions(soldier.Line,
                    mapObj.CurrentDirection == 'L' ? soldier.Index - 1 : soldier.Index + 1);
                mapObj.Hurdles.Add(pos);
            }

            soldier.UpdateXAxis(nextPos);
        }
    }

    private static bool IsBlocked(Map mapObj, int x, int y) =>
        x < 0 || x >= mapObj.MapData[0].Count || y < 0 || y >= mapObj.MapData.Count || mapObj.MapData[y][x] == '#';

    private static bool IsAtBoundary(Position soldier, Map mapObj) =>
        soldier.Line == 0 || soldier.Line == mapObj.MapData.Count - 1 ||
        soldier.Index == 0 || soldier.Index == mapObj.MapData[0].Count - 1;

    private static bool CheckLeftMovingDown(Position soldier, Map map)
    {
        var position = map.GetObstaclePositions()
            .FirstOrDefault(x => x.Line == soldier.Line && x.Index < soldier.Index);
        return
            map.CurrentDirection == 'D' &&
            map.MapData[soldier.Line][soldier.Index] == 'X' &&
            position != null &&
            soldier.Line == position.Line;
    }

    private static bool CheckRightMovingUp(Position soldier, Map map)
    {
        var position = map.GetObstaclePositions()
            .FirstOrDefault(x => x.Line == soldier.Line && x.Index > soldier.Index);
        return
            map.CurrentDirection == 'U' &&
            map.MapData[soldier.Line][soldier.Index] == 'X' &&
            position != null &&
            soldier.Line == position.Line;
    }

    private static bool CheckTopMovingLeft(Position soldier, Map map)
    {
        var position = map.GetObstaclePositions()
            .FirstOrDefault(x => x.Index == soldier.Index && x.Line < soldier.Line);
        return
            map.CurrentDirection == 'L' &&
            map.MapData[soldier.Line][soldier.Index] == 'X' &&
            position != null &&
            soldier.Index == position.Index;
    }

    private static bool CheckBottomMovingRight(Position soldier, Map map)
    {
        var position = map.GetObstaclePositions()
            .FirstOrDefault(x => x.Index == soldier.Index && x.Line > soldier.Line);
        return
            map.CurrentDirection == 'R' &&
            map.MapData[soldier.Line][soldier.Index] == 'X' &&
            position != null &&
            soldier.Index == position.Index;
    }
}