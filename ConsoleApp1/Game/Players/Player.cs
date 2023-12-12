using System.Drawing;

namespace ConsoleApp1.Game.Players;



public class Player
{
    public string Name;
    
    public ConsoleColor GhostColor;
    public ConsoleColor RealColor;
    
    public Position PositionPlayer;

    public int CountPoint = 0;
    
    public Player(ConsoleColor ghostColor, ConsoleColor realColor, Position positionPlayer, string name)
    {
        GhostColor = ghostColor;
        RealColor = realColor;
        PositionPlayer = positionPlayer;
        Name = name;
    }
    
    public Player(ConsoleColor ghostColor, ConsoleColor realColor, Position positionPlayer, string name, int countPoint): this(ghostColor, realColor, positionPlayer, name)
    {
        CountPoint = countPoint;
    }
    
    public void AddPoint(int value = 1) => CountPoint += value;

    public override string ToString()
    {
        return $"Name: {Name}, CountPoint: {CountPoint}, PositionPlayer: {PositionPlayer}";
    }
}