namespace ConsoleApp1.Game.Rules.Management;

public sealed class ManagementGame : Management
{

    public ManagementGame() : base()
    {
        // Жирная с широким углом правонаправленная стрелка
        Keys.Add(new KeyboardKey("\u2192", ConsoleColor.White, "Вправо"));
        Keys.Add(new KeyboardKey("\u2190", ConsoleColor.White, "Влево"));
        Keys.Add(new KeyboardKey("\u2191", ConsoleColor.White, "Вверх"));
        Keys.Add(new KeyboardKey("\u2193", ConsoleColor.White, "Вниз"));
        
        Keys.Add(new KeyboardKey("Space", ConsoleColor.Green, "Установить"));
        Keys.Add(new KeyboardKey("Enter", ConsoleColor.Red, "Пропуск"));
    }
    
}