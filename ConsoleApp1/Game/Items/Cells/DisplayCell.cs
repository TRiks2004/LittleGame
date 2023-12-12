using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;

namespace ConsoleApp1.Game.Items.Cells;

public class DisplayCell : Cell
{
    public DisplayCell(Mapping display, Player? responsible, Coordinates coordinatesCell) 
        : base(display, responsible, coordinatesCell)
    {
    }
}