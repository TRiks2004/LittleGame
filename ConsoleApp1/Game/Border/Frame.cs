using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;

namespace ConsoleApp1.Game.Border;

using ConsoleApp1.Game.Layers;


// рамка 
// Игра
// Доп. доп поле 
// окно с права 





public class Frame
{
    
    public static string GetOptions(int width, Player playerNow)
    {
        var name = $"{Symbol.GetCharFromSymbol(Mapping.VerticalLine)} " 
                   + playerNow.Name 
                   + $" {Symbol.GetCharFromSymbol(Mapping.VerticalLine)}";
        
        var text = "";

        text += 
            Symbol.GetCharFromSymbol(Mapping.VerticalCenterRight) + 
            
            new string(Symbol.GetCharFromSymbol(Mapping.HorizontalLine)[0], width - name.Length + 1) +
            Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterDown) +
            new string(Symbol.GetCharFromSymbol(Mapping.HorizontalLine)[0], name.Length - 2) + 
            Symbol.GetCharFromSymbol(Mapping.VerticalCenterLeft) + "\n";
        
        text += Symbol.GetCharFromSymbol(Mapping.VerticalLine) +
            new string(' ', width - name.Length + 1) +
            name + "\n";
        
        text += 
            Symbol.GetCharFromSymbol(Mapping.LowerLeftCorner) + 
            
            new string(Symbol.GetCharFromSymbol(Mapping.HorizontalLine)[0], width - name.Length + 1) +
            Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterUp) +
            new string(Symbol.GetCharFromSymbol(Mapping.HorizontalLine)[0], name.Length - 2) + 
            Symbol.GetCharFromSymbol(Mapping.LowerRightCorner);
        
        return text;
    } 
    
    
    public static void ConsoleWrite(Layers layers, Player playerNow)
    {
        Console.Clear();
        
        Console.SetCursorPosition(0,0);
        Console.Write(Symbol.GetCharFromSymbol(Mapping.UpperLeftCorner) +
                      new string(Symbol.GetCharFromSymbol(Mapping.HorizontalLine)[0], layers.SizeLayers.Width)
                      +Symbol.GetCharFromSymbol(Mapping.UpperRightCorner)
                      );
        
        for (int i = 0; i < layers.SizeLayers.Height; i++)
        {
            Console.SetCursorPosition(0,i + 1);
            Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
            
            Console.SetCursorPosition(layers.SizeLayers.Width + 1,i + 1);
            Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
        }
        
        Console.SetCursorPosition(0, layers.SizeLayers.Height + 1);
        Console.Write(GetOptions(layers.SizeLayers.Width, playerNow));
        
        Console.SetCursorPosition(0, layers.SizeLayers.Height + 1);
        Console.Write(GetOptions(layers.SizeLayers.Width, playerNow));
        
        
        layers.ConsoleWrite(new Shift(1, 1));
    }
    
    
    
}

