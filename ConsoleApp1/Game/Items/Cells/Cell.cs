using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;

namespace ConsoleApp1.Game.Items.Cells;

// DisplayCell
// RectangularCell

public abstract class Cell
{
    public Mapping Display { get; set; }
    public Player? Responsible { get; set; }

    public bool Ghost = false;

    public bool NotSpecialSymbol { get; set; }

    public Coordinates CoordinatesCell { get; set; }
    
    public Cell(Mapping display, Player? responsible, Coordinates coordinatesCell)
    {
        Display = display;
        Responsible = responsible;
        CoordinatesCell = coordinatesCell;
    }
    
    public string GetDisplay()
    {
        return Symbol.GetCharFromSymbol(Display);
    }

    public ConsoleColor GetColor()
    {
        if (Responsible == null) return ConsoleColor.White;
        return Ghost ? Responsible.GhostColor : Responsible.RealColor;
    }
    
    public void Clear()
    {
        Display = Mapping.Void;
        Responsible = null;
        Ghost = false;
    }
    
}