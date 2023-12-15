using ConsoleApp1.Game.Field;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Terms.Rules;

namespace ConsoleApp1.Game.Navigation;

public class Pade
{
    public static Size? SizePade { get; set; }
    
    public static int CountPlayers { get; set; }
    
    public Pade()
    {
        var consoleRules = new ConsoleRules();
        
        Console.CursorVisible = consoleRules.CursorVisible;
        
        SizePade = new Size(100, 30);
        CountPlayers = GameRules.DefaultCountPlayers;
        
        Navigation.NextField(NavigationMap.Start);

        while (true)
        {
        }
    }
    
    
    
}