namespace ConsoleApp1.Game.Items.Matrix;

public class Size
{
    #region Height
    private int _height;
    public int Height { 
        get => _height;
        set
        {
            if(value <= 0) throw new Exception("Height must be greater than 0");
            _height = value;
        } 
    }
    #endregion
    
    #region Width
    private int _width;
    public int Width { 
        get => _width;
        set
        {
            if(value <= 0) throw new Exception("Width must be greater than 0");
            _width = value;
        }
    }
    #endregion
    
    public Size(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static bool operator ==(Size size1, Size size2)
    {
        return size1.Width == size2.Width && size1.Height == size2.Height;
    }

    public static bool operator !=(Size size1, Size size2)
    {
        return !(size1 == size2);
    }

    public override string ToString()
    {
        return $"Width: {Width} x Height: {Height}";
    }

    public static Size operator +(Size size1, Size size2) => new Size(
        size1.Width + size2.Width, 
        size1.Height + size2.Height
    );
    
}