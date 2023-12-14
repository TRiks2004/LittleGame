namespace ConsoleApp1.Game.Items.Display;

public class Symbol
{
    public static string GetCharFromSymbol(Mapping symbol)
    {
        if (symbol == Mapping.Void) return " ";
        if (symbol == Mapping.VoidSquare) return "";

        if (symbol == Mapping.UpperRightCorner) return "\u2513";
        if (symbol == Mapping.UpperLeftCorner) return "\u250f";

        if (symbol == Mapping.LowerRightCorner) return "\u251b";
        if (symbol == Mapping.LowerLeftCorner) return "\u2517";
        
        if (symbol == Mapping.VerticalLine) return "\u2503";
        if (symbol == Mapping.HorizontalLine) return "━";
        
        if (symbol == Mapping.VerticalCenterRight) return "\u2523";
        if (symbol == Mapping.VerticalCenterLeft) return "\u252b";
        
        if (symbol == Mapping.HorizontallyCenterDown) return "\u2533";
        if (symbol == Mapping.HorizontallyCenterUp) return "\u253b";

        if (symbol == Mapping.VerticallyAndHorizontally) return "\u254b";
        
        throw new Exception("Symbol not found");
    }

}