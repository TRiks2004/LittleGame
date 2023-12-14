namespace ConsoleApp1.Game.Border.Container.ItemContainer;

public class ContainerLinePart
{
    private string _text = "";
    
    public string Text
    {
        get => _text;
        private set
        {
            _text = value;
            TextLength = value.Length;
        }
    }

    public int TextLength { get; set; }

    public ConsoleColor Color { get; set; }
    
    public ContainerLinePart(string text)
    {
        Text = text;
        Color = ConsoleColor.White;
    }
    
    public ContainerLinePart(string text, ConsoleColor color )
    {
        Text = text;
        Color = color;
    }
    
}