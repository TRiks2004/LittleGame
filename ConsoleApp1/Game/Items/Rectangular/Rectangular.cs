using System.Diagnostics;
using ConsoleApp1.Game.Items.Cells;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;

namespace ConsoleApp1.Game.Items.Rectangular;





public class Rectangular
{
    public Size SizeRectangular;
    public List<List<RectangularCell>> RectangularPoint = new ();

    public Player Responsible;
    
    private Shift _shiftRectangular; // Используем поле вместо автоматического свойства

    public Shift ShiftRectangular
    {
        get { return _shiftRectangular; }
        set { _shiftRectangular = value; }
    }
    public Coordinates CoordinatesParam { get; set; }
    
    // _rectangularsSize.Width = _sealBone.Throw() + 4;
    // _rectangularsSize.Height = _sealBone.Throw() + 3;
    
    public static readonly Size DefaultSizeRectangularPlus = new (4, 3);
    
    public Rectangular(Size sizeRectangular, Player player, Shift? shift = null, Coordinates? coor = null)
    {
        Responsible = player;
        
        SizeRectangular = sizeRectangular;
        
        CoordinatesParam = coor ?? new Coordinates(0, 0);
        ShiftRectangular = shift ?? new Shift(0, 0);
        
        for (int i = 0; i < SizeRectangular.Height; i++)
        {
            RectangularPoint.Add(new List<RectangularCell>());
            
            for (int j = 0; j < SizeRectangular.Width; j++)
            {
                RectangularCell rectangularCell;
                
                var coordinates = new Coordinates(
                    CoordinatesParam.X + j, 
                    CoordinatesParam.Y + i
                );
                
                Mapping symbol;
                
                #region Генирация прямоугольника
                if (i == 0 && j == 0) {
                    symbol = Mapping.UpperLeftCorner; 
                }
                else if (i == 0 && j == SizeRectangular.Width - 1) {
                    symbol = Mapping.UpperRightCorner;
                }
                else if (i == SizeRectangular.Height - 1 && j == 0) {
                    symbol = Mapping.LowerLeftCorner;
                }
                else if (i == SizeRectangular.Height - 1 && j == SizeRectangular.Width - 1) {
                    symbol = Mapping.LowerRightCorner;
                }
                else if (i == 0) {
                    symbol = Mapping.HorizontalLine;
                }
                else if (i == SizeRectangular.Height - 1) {
                    symbol = Mapping.HorizontalLine;
                }
                else if (j == SizeRectangular.Width - 1) {
                    symbol = Mapping.VerticalLine;
                }
                else if (j == 0) {
                    symbol = Mapping.VerticalLine;
                }
                else {
                    symbol = Mapping.VoidSquare;
                }
                #endregion
                
                rectangularCell = new RectangularCell(symbol, Responsible, coordinates);
                
                RectangularPoint[i].Add(rectangularCell);
            
            }
        }
        
        
    }

    private void UpdateCoordinates()
    {
        CoordinatesParam.SetShift(ShiftRectangular); 
    }
    
    #region Логика Движения прямоугольника

  
    
    private void GoVertical(int value)
    {
        ShiftRectangular.ShiftY += value;
        UpdateCoordinates();
    }

    private void GoHorizontal(int value)
    {
        ShiftRectangular.ShiftX += value;
        UpdateCoordinates();
    }

    public void GoUp(int value = 1) => GoVertical(-value);

    public void GoDown(int value = 1) => GoVertical(value);
    
    public void GoLeft(int value = 1) => GoHorizontal(-value);
    
    public void GoRight(int value = 1) => GoHorizontal(+value);

    #endregion
    
    public IEnumerable<List<RectangularCell>> GetRectangular()
    {
        foreach (var rectangularCell in RectangularPoint.SelectMany(rectangularCells => rectangularCells))
        {
            rectangularCell.CoordinatesCell.SetShift(ShiftRectangular);
        }
        
        ShiftRectangular.ShiftX = 0;
        ShiftRectangular.ShiftY = 0;
        
        return RectangularPoint;
        
    }
    
    public int GetSquare() => SizeRectangular.Width * SizeRectangular.Height;
    
}