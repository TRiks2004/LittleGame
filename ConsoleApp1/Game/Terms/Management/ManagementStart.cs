using System.Globalization;
using System.Text.Unicode;

namespace ConsoleApp1.Game.Terms.Management;

public class ManagementStart : Management
{
    public ManagementStart() : base()
    {
        Keys.Add(new KeyboardKey(SymbolManagement.Up, ConsoleColor.White, "Вверх"));
        Keys.Add(new KeyboardKey(SymbolManagement.Down, ConsoleColor.White, "Вниз"));
        
        Keys.Add(new KeyboardKey(SymbolManagement.Enter, ConsoleColor.Red, "Выбрать"));
        
    }
    
}