namespace ConsoleApp1.Game.Players;

public class Position
{
    public bool UpCorner { get; set; }
    public bool LeftCorner { get; set; }

    public Position(bool upCorner, bool leftCorner)
    {
        UpCorner = upCorner;
        LeftCorner = leftCorner;
    }
    
    public static Position UpLeftCorner() => new Position(true, true);
    public static Position DownRightCorner() => new Position(false, false);
    
    public static Position UpRightCorner() => new Position(true, false);
    public static Position DownLeftCorner() => new Position(false, true);
    
    
    public static Position GetPosition(int number)
    {
        return number switch
        {
            0 => UpLeftCorner(),
            1 => DownRightCorner(),
            2 => UpRightCorner(),
            3 => DownLeftCorner(),
            _ => throw new Exception("Wrong number")
        };
    }
    
    public override string ToString()
    {
        return $"UpCorner: {UpCorner}, LeftCorner: {LeftCorner}";
    }
    
    
}