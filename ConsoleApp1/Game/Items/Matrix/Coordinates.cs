namespace ConsoleApp1.Game.Items.Matrix;

public class Coordinates
{
    public int X;
    public int Y;
    
    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void SetShift(Shift shift)
    {
        X += shift.ShiftX;
        Y += shift.ShiftY;
    }
}