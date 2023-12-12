using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;

namespace ConsoleApp1.Game.Items.Cells;

public class RectangularCell: Cell
{
    public RectangularCell(Mapping display, Player? responsible, Coordinates coordinatesCell) 
        : base(display, responsible, coordinatesCell)
    {
    }

    public RectangularCell(Mapping display, Player? responsible, Coordinates coordinatesCell, int yu) 
        : this(display, responsible, coordinatesCell)
    {
        
    }
}