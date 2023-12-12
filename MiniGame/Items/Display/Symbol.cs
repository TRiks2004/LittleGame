namespace MiniGame.Items.Display;

public class Symbol
{
    public static string GetCharFromSymbol(Mapping symbol)
    {
        if (symbol == Mapping.Void) return " ";
        if (symbol == Mapping.VoidSquare) return " ";

        if (symbol == Mapping.UpperRightCorner) return "┓";
        if (symbol == Mapping.UpperLeftCorner) return "┏";

        if (symbol == Mapping.LowerRightCorner) return "┛";
        if (symbol == Mapping.LowerLeftCorner) return "┗";
        if (symbol == Mapping.VerticalLine) return "┃";
        if (symbol == Mapping.HorizontalLine) return "━";

        throw new Exception("Symbol not found");
    }

}