using ConsoleApp1.Game.Field;

namespace ConsoleApp1.Game.Navigation;

public static class Navigation
{
 
    public static Field.Field? CurrentField { get; set; }
    
    public static void NextField(NavigationMap navigationMap)
    {
        if (Pade.SizePade == null) return;
        
        switch (navigationMap)
        {
            case NavigationMap.Start: CurrentField = new StartField(Pade.SizePade); break;
            case NavigationMap.Game: CurrentField = new GameField(Pade.SizePade, Pade.CountPlayers); break;
            case NavigationMap.Rules: CurrentField = new RulesField(Pade.SizePade); break;
            case NavigationMap.Exit:
                break;
            case NavigationMap.Menu:
                break;
            case NavigationMap.EndGame:
                break;
            case NavigationMap.SettingStartGame:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(navigationMap), navigationMap, null);
        }
        
        
    }
    
    
}