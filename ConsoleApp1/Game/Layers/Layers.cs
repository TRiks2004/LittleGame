using ConsoleApp1.Game.Items.Cells;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;

namespace ConsoleApp1.Game.Layers;

public abstract class Layers
{
    public List<List<Cell>> Matrix = new List<List<Cell>>();

    public Size SizeLayers;
    
    public Layers(Size size)
    {
        SizeLayers = size;
        
        for (int i = 0; i < SizeLayers.Height; i++)
        {
            Matrix.Add(new List<Cell>());
            for (int j = 0; j < SizeLayers.Width; j++)
            {
                Matrix[i].Add(new DisplayCell(Mapping.Void, null, new Coordinates(j, i)));
            }
        }
    }
    
    public void Clear()
    {
        for (int i = 0; i < SizeLayers.Height; i++)
        {
            for (int j = 0; j < SizeLayers.Width; j++)
            {
                Matrix[i][j] = new DisplayCell(Mapping.Void, null, new Coordinates(j, i));
            }
        }
    }

    public void ConsoleWrite(Shift? shift = null)
    {
        shift ??= new Shift(0, 0);

        foreach (var cell in Matrix.SelectMany(cells => cells))
        {
            Console.SetCursorPosition(
                cell.CoordinatesCell.X + shift.ShiftX, 
                cell.CoordinatesCell.Y + shift.ShiftY
            );
            Console.ForegroundColor = cell.GetColor();
            
            Console.Write(cell.GetDisplay());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
