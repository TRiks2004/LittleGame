namespace ConsoleApp1.Game.Items.Matrix;

public class Shift
{
    private int _shiftX;
    private int _shiftY;
    
    public int ShiftX { 
        get => _shiftX; 
        set => _shiftX = value; 
    }

    public int ShiftY
    {
        get => _shiftY;
        set => _shiftY = value;
    }
    
    public Shift(int shiftX, int shiftY)
    {
        ShiftX = shiftX;
        ShiftY = shiftY;
    }
    
}