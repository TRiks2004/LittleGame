namespace ConsoleApp1.Game.Terms.Rules;

public class ConsoleRules : Rules
{
    public bool CursorVisible { get; set; } = false;
    
    public ConsoleRules() { }
    
    public ConsoleRules(bool cursorVisible)
    {
        CursorVisible = cursorVisible;
    }
    
    
    public override string ToString()
    {
        return $"Rules: StirredEachOther => {CursorVisible}";
    }
}