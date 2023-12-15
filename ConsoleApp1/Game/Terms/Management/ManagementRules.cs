namespace ConsoleApp1.Game.Terms.Management;

public class ManagementRules : Management
{
    public ManagementRules()
    {
        Keys.Add(new KeyboardKey(SymbolManagement.Left, ConsoleColor.White, "Влево"));
        Keys.Add(new KeyboardKey(SymbolManagement.Right, ConsoleColor.White, "Вправо"));
        Keys.Add(new KeyboardKey(SymbolManagement.Esc, ConsoleColor.DarkCyan, "Назад"));
    }
    
}