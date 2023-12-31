namespace ConsoleApp1.Game.Terms.Management;



public abstract class Management
{
    protected List<KeyboardKey> Keys { get; set; } = new List<KeyboardKey>();
    
    public List<KeyboardKey> GetKeys() => Keys;
}