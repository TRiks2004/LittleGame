using KeyBoard;
using MiniGame.Items.Display;

namespace MiniGame;


class Player
{
    public ConsoleColor GhostColor;
    public ConsoleColor RealColor;
    
    public bool SpaceUp;
    public bool SpaceLeft;
    
    
    public Player(ConsoleColor ghostColor, ConsoleColor realColor, bool spaceUp, bool spaceLeft)
    {
        GhostColor = ghostColor;
        RealColor = realColor;
        SpaceUp = spaceUp;
        SpaceLeft = spaceLeft;
    }
    
    
}
class Pixel
{
    public Mapping SymbolIndex{get; set;}

    public int X;
    public int Y;
    
    public bool Ghost = true;

    public Player? Player;
    
    public Pixel(Mapping symbolIndex, int x, int y)
    {
        SymbolIndex = symbolIndex;
        X = x;
        Y = y;
    }
    
    public Pixel(Mapping symbolIndex, int x, int y, Player? player): this(symbolIndex, x, y)
    {
        Player = player;
    }
    
    
    public string GetSymbol()
    {
        try
        {
            return Symbol.GetCharFromSymbol(SymbolIndex);
        }
        catch (Exception e) { return " "; }
        
    }
    
    public ConsoleColor GetColor()
    {
        
        if (Player == null) return ConsoleColor.Blue;
        return Ghost ? Player.GhostColor : Player.RealColor;
    }
    
}

class Rectangular
{
    public int X{get; set;}
    public int Y{get; set;}
    public int Width{get; set;}
    public int Height{get; set;}
    
    public List<List<Pixel>> RectangularPoint = new ();

    public Player Player;
    
    public Rectangular(int width, int height, int x, int y, Player player)
    {
        X = x;
        Y = y;

        Player = player;
        
        Width = width;
        Height = height;
        
        Pixel pixel; 
        
        for (int i = 0; i < height; i++)
        {
            RectangularPoint.Add(new List<Pixel>());
            for (int j = 0; j < width; j++)
            {
                if (i == 0 && j == 0) pixel = new Pixel(
                    Mapping.UpperLeftCorner, j, i, player);
                else if (i == 0 && j == width - 1) pixel = new Pixel(
                    Mapping.UpperRightCorner, j, i, player);
                
                else if (i == height - 1 && j == 0) pixel = new Pixel(
                    Mapping.LowerLeftCorner, j, i, player);
                else if (i == height - 1 && j == width - 1) pixel = new Pixel(
                    Mapping.LowerRightCorner, j, i, player);

                else if (i == 0) pixel = new Pixel(
                    Mapping.HorizontalLine, j, i, player);
                else if (i == height - 1) pixel = new Pixel(
                    Mapping.HorizontalLine, j, i, player);

                else if (j == width - 1) pixel = new Pixel(
                    Mapping.VerticalLine, j, i, player);
                else if (j == 0) pixel = new Pixel(
                    Mapping.VerticalLine, j, i, player);
                
                else pixel = new Pixel(
                    Mapping.VoidSquare, j, i);
                
                RectangularPoint[i].Add(pixel);
            
            }
        }
    }
    
    public List<List<Pixel>> GetRectangularPoint()
    {

        var rectangularPointOut = new List<List<Pixel>>();

        for (var index = 0; index < RectangularPoint.Count; index++)
        {
            var pixels = RectangularPoint[index];
            
            rectangularPointOut.Add(new List<Pixel>());

            foreach (var pixel in pixels)
            {
                rectangularPointOut[index].Add(new Pixel(pixel.SymbolIndex, pixel.X + X, pixel.Y + Y, pixel.Player));
            }
        }

        return rectangularPointOut;
    }
    
}






class Spase
{
    private bool playerUp = true;
    private bool playerLeft = true;
    
    public readonly List<List<Pixel>> SpaseOut;
    public readonly List<List<Pixel>> FirstLayer;
    public readonly List<List<Pixel>> SecondLayer;

    private Rectangular _rectangular;

    public int Width;
    public int Height;

    List<Player> Players = new ();
    int PlayerIndex = 0;
    
    bool rullsSpawnIgnore = false;
    public Spase(int width, int height)
    {
        Width = width;
        Height = height;
        
        Players.Add(new Player(ConsoleColor.Cyan, ConsoleColor.Yellow, true, true));
        Players.Add(new Player(ConsoleColor.Red, ConsoleColor.Blue, false, false));
        
        SpaseOut = new List<List<Pixel>>();
        FirstLayer = new List<List<Pixel>>();
        SecondLayer = new List<List<Pixel>>();
        
        for(var i = 0; i < height; i++)
        {
            SpaseOut.Add(new List<Pixel>());
            FirstLayer.Add(new List<Pixel>());
            SecondLayer.Add(new List<Pixel>());

            for (var j = 0; j < width; j++)
            {
                SpaseOut[i].Add(new Pixel(0, j, i));
                FirstLayer[i].Add(new Pixel(0, j, i));
                SecondLayer[i].Add(new Pixel(0, j, i));
            }
        }

        var listener = new KeyBoardListener();
        
        listener.OnKeyPressSpace += ListenerOnOnKeyPressSpace;
        listener.OnKeyPressEnter += ListenerOnOnKeyPressEnter;
        
        listener.OnKeyPressDown += ListenerOnOnKeyPressDown;
        listener.OnKeyPressUp += ListenerOnOnKeyPressUp;
        listener.OnKeyPressLeft += ListenerOnOnKeyPressLeft;
        listener.OnKeyPressRight += ListenerOnOnKeyPressRight;

        listener.OnKeyPress += ShowDraw;
        
        listener.StartKeyBoardListener();
        
        
        _rectangular = GetRectangular();
        
        rullsSpawnIgnore = true;
        
        ListenerOnOnKeyPressSpace();
        ListenerOnOnKeyPressSpace();
        
        rullsSpawnIgnore = false;


    }

    private void ListenerOnOnKeyPressRight()
    {
        if (_rectangular.X + _rectangular.Width < SpaseOut[0].Count)
        {
            _rectangular.X ++;
        }
    }

    private void ListenerOnOnKeyPressLeft()
    {
        if (_rectangular.X > 0)
        {
            _rectangular.X --;
        }
        
    }

    private void ListenerOnOnKeyPressUp()
    {
        if (_rectangular.Y > 0)
        {
            _rectangular.Y --;
        }
        
        
    }

    private void ListenerOnOnKeyPressDown()
    {
        if (_rectangular.Y + _rectangular.Height < SpaseOut.Count)
        {
            _rectangular.Y ++;
        }
        
    }

    private void ListenerOnOnKeyPressEnter() { }

    
    public bool CheckRectangularTouch(Rectangular _rectangular)
    {
        // Check above the rectangular area
        for (var i = 0; i < _rectangular.Width; i++)
        {
            int targetY = _rectangular.Y - 1;
            int targetX = _rectangular.X + i;

            if (IsPixelWithinBounds(targetY, targetX))
            {
                if (SecondLayer[targetY][targetX].Player == _rectangular.Player)
                {
                    return true;
                }
            }
        }

        // Check below the rectangular area
        for (var i = 0; i < _rectangular.Width; i++)
        {
            int targetY = _rectangular.Y + _rectangular.Height;
            int targetX = _rectangular.X + i;

            if (IsPixelWithinBounds(targetY, targetX))
            {
                if (SecondLayer[targetY][targetX].Player == _rectangular.Player)
                {
                    return true;
                }
            }
        }

        // Check to the left of the rectangular area
        for (var i = 0; i < _rectangular.Height; i++)
        {
            int targetY = _rectangular.Y + i;
            int targetX = _rectangular.X - 1;

            if (IsPixelWithinBounds(targetY, targetX))
            {
                if (SecondLayer[targetY][targetX].Player == _rectangular.Player)
                {
                    return true;
                }
            }
        }

        // Check to the right of the rectangular area
        for (var i = 0; i < _rectangular.Height; i++)
        {
            int targetY = _rectangular.Y + i;
            int targetX = _rectangular.X + _rectangular.Width;

            if (IsPixelWithinBounds(targetY, targetX))
            {
                if (SecondLayer[targetY][targetX].Player == _rectangular.Player)
                {
                    return true;
                }
            }
        }

        return false;
    }


    private bool IsPixelWithinBounds(int y, int x)
    {
        return y >= 0 && y < Height && x >= 0 && x < Width;
    }
    
    
    private void ListenerOnOnKeyPressSpace()
    {
        foreach (var pixels in SecondLayer)
        {
            foreach (var pixel in pixels)
            {
                Console.Write(pixel.Player == null? 0: 1);
            }
            Console.WriteLine();
        }
        
        var point = _rectangular.GetRectangularPoint();

        if (!rullsSpawnIgnore)
        {
            if (point.Any(t => t.Any(t1 => SecondLayer[t1.Y][t1.X].Player != null ))) { return; }

            if (!CheckRectangularTouch(_rectangular)) { return; }
        }

        var player = SecondLayer[point[0][0].Y][point[0][0].X].Player;
        
        Console.WriteLine(player == null? "null" : player);
        
        foreach (var pixel in point.SelectMany(pixels => pixels))
        {
            pixel.Ghost = false;
            
            if (pixel.SymbolIndex == Mapping.VoidSquare)
            {
                SecondLayer[pixel.Y][pixel.X].Player = pixel.Player;
            }
            else
            {
                SecondLayer[pixel.Y][pixel.X] = pixel;
            }
        }
        
       
        if (PlayerIndex < Players.Count - 1)
        {
            PlayerIndex++;
        }
        else
        {
            PlayerIndex = 0;
        }
        
        
        _rectangular = GetRectangular();
        
    }


    private void ClearFirstLayer()
    {
        for (var i = 0; i < FirstLayer.Count; i++)
        {
            for (var j = 0; j < FirstLayer[i].Count; j++)
            {
                FirstLayer[i][j] = new Pixel(0, j, i);
            }
        }
    }
    
    private Rectangular GetRectangular()
    {
        var random = new Random();
        var width = random.Next(3, 10);
        var height = random.Next(3, 10);

        int x, y;
        
        if (Players[PlayerIndex].SpaceLeft) x = 0;
        else x = Width - width;
        
        if (Players[PlayerIndex].SpaceUp) y = 0;
        else y = Height - height;
        
        return new Rectangular(width, height, x, y, Players[PlayerIndex]);
    }
    
    
    
    
    public void Draw()
    {
        foreach (var pixel in _rectangular.GetRectangularPoint().SelectMany(pixels => pixels))
        {
            FirstLayer[pixel.Y][pixel.X] = pixel;
        }
        
        for (var i = 0; i < SpaseOut.Count; i++)
        {
            for (var j = 0; j < SpaseOut[i].Count; j++)
            {
                if (FirstLayer[i][j].SymbolIndex == Mapping.Void) SpaseOut[i][j] = SecondLayer[i][j];
                else if (FirstLayer[i][j].SymbolIndex == Mapping.VoidSquare) SpaseOut[i][j] = SecondLayer[i][j];
                else SpaseOut[i][j] = FirstLayer[i][j];
            }
        }
        
        ClearFirstLayer();

        for (var index = 0; index < SpaseOut.Count; index++)
        {
            var pix = SpaseOut[index];
            
            for (var j = 0; j < pix.Count - 1; j++)
            {
                var play = SecondLayer[index][j].Player;

                var pixX = pix[j].X;
                var pixY = pix[j].Y;
                
                
                
                if (
                    pix[j].SymbolIndex == Mapping.VerticalLine
                    && pix[j + 1].SymbolIndex == Mapping.VerticalLine)
                {
                    pix[j] = new Pixel(Mapping.VoidSquare, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.VoidSquare, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.UpperRightCorner
                    && pix[j + 1].SymbolIndex == Mapping.UpperLeftCorner)
                {
                    pix[j] = new Pixel(Mapping.HorizontalLine, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.HorizontalLine, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.LowerRightCorner
                    && pix[j + 1].SymbolIndex == Mapping.LowerLeftCorner)
                {
                    pix[j] = new Pixel(Mapping.HorizontalLine, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.HorizontalLine, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.UpperRightCorner
                    && pix[j + 1].SymbolIndex == Mapping.VerticalLine)
                {
                    pix[j] = new Pixel(Mapping.HorizontalLine, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.LowerRightCorner, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.VerticalLine
                    && pix[j + 1].SymbolIndex == Mapping.LowerLeftCorner)
                {
                    pix[j] = new Pixel(Mapping.UpperLeftCorner, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.HorizontalLine, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.LowerRightCorner
                    && pix[j + 1].SymbolIndex == Mapping.VerticalLine)
                {
                    pix[j] = new Pixel(Mapping.HorizontalLine, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.UpperRightCorner, pixX + 1, pixY, play);
                }

                if (
                    pix[j].SymbolIndex == Mapping.VerticalLine
                    && pix[j + 1].SymbolIndex == Mapping.UpperLeftCorner)
                {
                    pix[j] = new Pixel(Mapping.LowerLeftCorner, pixX, pixY, play);
                    pix[j + 1] = new Pixel(Mapping.HorizontalLine, pixX + 1, pixY, play);
                }

                pix[j].Ghost = false;
                pix[j + 1].Ghost = false;
                
            }
        }

        for (var index = 0; index < SpaseOut.Count - 1; index++)
        {
            var pix1 = SpaseOut[index];
            var pix2 = SpaseOut[index + 1];
            
            for (var j = 0; j < SpaseOut[index].Count; j++)
            {
                var play = SecondLayer[index][j].Player;

                var pixX = pix1[j].X;
                var pixY = pix1[j].Y;
                
                if (
                    pix1[j].SymbolIndex == Mapping.HorizontalLine
                    && pix2[j].SymbolIndex == Mapping.HorizontalLine)
                {
                    pix1[j] = new Pixel(Mapping.VoidSquare, pixX, pixY, play);
                    pix2[j] = new Pixel(Mapping.VoidSquare, pixX, pixY + 1, play);
                }
                
                if (
                    pix1[j].SymbolIndex == Mapping.HorizontalLine
                    && pix2[j].SymbolIndex == Mapping.HorizontalLine)
                {
                    pix1[j] = new Pixel(Mapping.VoidSquare, pixX, pixY, play);
                    pix2[j] = new Pixel(Mapping.VoidSquare, pixX, pixY + 1, play);
                }
            }
        }
    }
    
    public void ShowDraw()
    {
        Draw();
        
        Console.Clear();
        
        foreach (var pixels in SpaseOut)
        {
            foreach (var pixel in pixels)
            {
                Console.SetCursorPosition(pixel.X, pixel.Y);
                Console.ForegroundColor = pixel.GetColor();
                Console.Write(pixel.GetSymbol());

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        
        Console.SetCursorPosition(0, 32);
        Console.WriteLine($"X: {_rectangular.X} Y: {_rectangular.Y} W: {_rectangular.Width} H: {_rectangular.Height}");
    }
    
    
    
    
}




internal static class Program
{
    
    
    

    private const int Width = 80;
    private const int Height = 30;

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        
        var spase = new Spase(Width, Height);
        
        spase.Draw();
        spase.ShowDraw();


        while (true) ;
    }
}


