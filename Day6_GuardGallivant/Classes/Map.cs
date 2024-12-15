namespace Day6_GuardGallivant.Classes;

public class Map
{
    public List<List<char>> MapData { get; set; }
    public char CurrentDirection { get; set; }
    public int MovingSteps { get; set; }
    public List<Position> Obstacles { get; set; }
    public List<Position> Hurdles { get; set; }

    private Map(List<List<char>> mapData, char currentDirection = 'U', int movingSteps = 0)
    {
        MapData = mapData;
        CurrentDirection = currentDirection;
        MovingSteps = movingSteps;
        
        Obstacles = [];
        Hurdles = [];
    }

    public List<List<char>> GetMap() => MapData;
    public static Map Create(List<List<char>> map) => new(map);
}