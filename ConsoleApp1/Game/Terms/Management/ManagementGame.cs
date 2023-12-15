namespace ConsoleApp1.Game.Terms.Management;

public sealed class ManagementGame : Management
{

    public ManagementGame() : base()
    {
        // Жирная с широким углом правонаправленная стрелка
        Keys.Add(new KeyboardKey(SymbolManagement.Left, ConsoleColor.White, "Вправо"));
        Keys.Add(new KeyboardKey(SymbolManagement.Right, ConsoleColor.White, "Влево"));
        
        Keys.Add(new KeyboardKey(SymbolManagement.Up, ConsoleColor.White, "Вверх"));
        Keys.Add(new KeyboardKey(SymbolManagement.Down, ConsoleColor.White, "Вниз"));
        
        Keys.Add(new KeyboardKey(SymbolManagement.Space, ConsoleColor.Green, "Установить"));
        Keys.Add(new KeyboardKey(SymbolManagement.Enter, ConsoleColor.Red, "Пропуск"));

        

    }
    
}