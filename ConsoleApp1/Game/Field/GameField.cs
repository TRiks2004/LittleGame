using ConsoleApp1.Game.Border;
using ConsoleApp1.Game.Items.Cells;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Items.Rectangular;
using ConsoleApp1.Game.Items.SealBone;
using ConsoleApp1.Game.Layers;
using ConsoleApp1.Game.Players;
using ConsoleApp1.Game.Terms.Management;
using ConsoleApp1.Game.Terms.Rules;
using KeyBoard;

namespace ConsoleApp1.Game.Field;

public class GameField : Field
{
    private readonly TopLayer _topLayer;
    private readonly BottomLayer _bottomLayer;

    private readonly List<Player> _players = new List<Player>();
    
    private int _indexPlayerNow = 0;

    #region Цвета поумолчанию для игроков

    private readonly List<ConsoleColor> _ghostColors = new ()
    {
        ConsoleColor.DarkBlue,
        ConsoleColor.DarkGreen,
        ConsoleColor.DarkRed,
        ConsoleColor.DarkYellow
    };

    private readonly List<ConsoleColor> _realColors = new ()
    {
        ConsoleColor.Blue,
        ConsoleColor.Green,
        ConsoleColor.Red,
        ConsoleColor.Yellow
    };
    #endregion

    private readonly List<Rectangular> _rectangulars = new();
    private int _rectangularsIndex = 0;
    private Size _rectangularsSize;
    
    private SealBone _sealBone = new SealBone(6);
    
    private readonly GameRules _rules;
    
    private bool _isEnd = false;
    private int _pass;
    
    
    public GameField(Size sizeGameField, int countPlayers, GameRules? gameRules = null) : base(sizeGameField)
    {
        _rules = gameRules ?? new GameRules();

        Management = new ManagementGame();
        
        _topLayer = new TopLayer(SizeGameField);
        _bottomLayer = new BottomLayer(SizeGameField);
        
        #region Инициализация игроков
        
        for (int i = 0; i < countPlayers; i++)
        {
            _players
                .Add(
                    new Player(
                        _ghostColors[i], 
                        _realColors[i], 
                        Position.GetPosition(i), 
                        $"Игрок {i+1}"
                    )
                );
        }
        
        #endregion
        
        _rectangularsSize = new Size(4, 3);
        _rectangulars.Add(new Rectangular(_rectangularsSize, _players[_indexPlayerNow]));
        
        #region Инициализация клавиатуры
        
        Listener.OnKeyPress += ConsoleWrite;
        
        Listener.OnKeyPressUp += ListenerOnOnKeyPressUp;
        Listener.OnKeyPressDown +=ListenerOnOnKeyPressDown;
        Listener.OnKeyPressRight += ListenerOnOnKeyPressRight; 
        Listener.OnKeyPressLeft += ListenerOnOnKeyPressLeft;
        
        Listener.OnKeyPressSpace += ListenerOnOnKeyPressSpace;
        Listener.OnKeyPressEnter += ListenerOnOnKeyPressEnter;
        
        Listener.StartKeyBoardListener();

        #endregion
        
        
        for (int i = 0; i < countPlayers; i++)
        {
            _rules.StirredNextSimilarOne = false;
            
            ListenerOnOnKeyPressSpace();
            
            _rules.StirredNextSimilarOne = true;
        }
        
        ConsoleWrite();
        
    }

    private void ListenerOnOnKeyPressEnter()
    {
        _pass++;
        SpawnRectangular();
        
        if (_pass == _players.Count)
        {
            _isEnd = true;
            Log.AddLn("Игра окончена");
        }
    }


    private void SpawnRectangular()
    {
        _rectangularsIndex++;
        
        NextPlayer();

        _rectangularsSize.Width = _sealBone.Throw() + Rectangular.DefaultSizeRectangularPlus.Width;
        _rectangularsSize.Height = _sealBone.Throw() + Rectangular.DefaultSizeRectangularPlus.Height;
        
        var cords = new Coordinates(0, 0);

        if (!_players[_indexPlayerNow].PositionPlayer.UpCorner)
        {
            cords.Y = SizeGameField.Height - _rectangularsSize.Height;
        }
        
        if (!_players[_indexPlayerNow].PositionPlayer.LeftCorner)
        {
            cords.X  = SizeGameField.Width - _rectangularsSize.Width;
        } 
        
        _rectangulars.Add(new Rectangular(
            _rectangularsSize, 
            _players[_indexPlayerNow],
            null,
            cords
        ));

        
    }
    
    private bool CheckRectangularTouch()
    {
        var x = _rectangulars[_rectangularsIndex].CoordinatesParam.X;
        var y = _rectangulars[_rectangularsIndex].CoordinatesParam.Y;
        
        var outher = 0;

        // Cлева
        outher = x - 1;
        if (outher >= 0)
            for (var i = y; i < y + _rectangulars[_rectangularsIndex].SizeRectangular.Height; i++)
            {
                if (_bottomLayer.Matrix[i][outher].Responsible == _rectangulars[_rectangularsIndex].Responsible)
                {
                    return true;
                }
            }

        // Сверху
        outher = y - 1; 
        if (outher >= 0)
            for (var i = x; i < x + _rectangulars[_rectangularsIndex].SizeRectangular.Width; i++)
            {
                if (_bottomLayer.Matrix[outher][i].Responsible == _rectangulars[_rectangularsIndex].Responsible)
                {
                    return true;
                }
            }

        // Справа
        outher = x + _rectangulars[_rectangularsIndex].SizeRectangular.Width;
        if (outher < SizeGameField.Width)
            for (var i = y; i < y + _rectangulars[_rectangularsIndex].SizeRectangular.Height; i++)
            {
                if (_bottomLayer.Matrix[i][outher].Responsible == _rectangulars[_rectangularsIndex].Responsible)
                {
                    return true;
                }
            }

        // Снизу
        outher = y + _rectangulars[_rectangularsIndex].SizeRectangular.Height;
        if (outher < SizeGameField.Height)
            for (var i = x; i < x + _rectangulars[_rectangularsIndex].SizeRectangular.Width; i++)
            {
                if (_bottomLayer.Matrix[y + _rectangulars[_rectangularsIndex].SizeRectangular.Height][i].Responsible == _rectangulars[_rectangularsIndex].Responsible)
                {
                    return true;
                }
            }
            
        return false;
    }
    
    private void ListenerOnOnKeyPressSpace()
    {
        var rectangularCells = _rectangulars[_rectangularsIndex].GetRectangular();
        
        // Условие для отключения спавна квадратов
        if(!_rules.StirredEachOther)
            foreach (var rectangular in rectangularCells.SelectMany(t => t))
                if (
                    _bottomLayer
                        .Matrix[rectangular.CoordinatesCell.Y][rectangular.CoordinatesCell.X]
                        .Display != Mapping.Void
                ) return;

        if (_rules.StirredNextSimilarOne) if (!CheckRectangularTouch()) return;
        
        foreach (var rectangular in rectangularCells.SelectMany(t => t))
        {
            rectangular.Ghost = !rectangular.Ghost;
            _bottomLayer
                .Matrix[rectangular.CoordinatesCell.Y][rectangular.CoordinatesCell.X] = rectangular;
        }
        
        _players[_indexPlayerNow].AddPoint(_rectangulars[_rectangularsIndex].GetSquare());
        
        SpawnRectangular();
        
        _pass = 0;
        
    }

    #region Управление Квадратом
    
    private void ListenerOnOnKeyPressLeft()
    {
        if(_rectangulars[_rectangularsIndex].CoordinatesParam.X == 0) return;
        _rectangulars[_rectangularsIndex].GoLeft();
    }

    private void ListenerOnOnKeyPressUp()
    {
        if(_rectangulars[_rectangularsIndex].CoordinatesParam.Y == 0) return;
        _rectangulars[_rectangularsIndex].GoUp();
    }

    private void ListenerOnOnKeyPressDown()
    {
        if(_rectangulars[_rectangularsIndex].CoordinatesParam.Y 
            + _rectangulars[_rectangularsIndex].SizeRectangular.Height >= SizeGameField.Height) return;
        _rectangulars[_rectangularsIndex].GoDown();
    }

    private void ListenerOnOnKeyPressRight()
    {
        if(_rectangulars[_rectangularsIndex].CoordinatesParam.X
            + _rectangulars[_rectangularsIndex].SizeRectangular.Width >= SizeGameField.Width) return;
        _rectangulars[_rectangularsIndex].GoRight();
    }

    #endregion

    public void NextPlayer()
    {
        _indexPlayerNow = _indexPlayerNow + 1 < _players.Count ? _indexPlayerNow + 1 : 0; 
    }

    public sealed override void ConsoleWrite()
    {
        Console.Clear();
        
        var rectangularCells = _rectangulars[_rectangularsIndex].GetRectangular();
        
        foreach (var rectangular in rectangularCells.SelectMany(t => t))
        {
            _topLayer.Matrix[rectangular.CoordinatesCell.Y][rectangular.CoordinatesCell.X] = rectangular;
        }

        ChangeSymbolAll();
        
        var baseLayers = _topLayer + _bottomLayer;
        
        _topLayer.Clear();

        Frame.ConsoleWrite(baseLayers, _players[_indexPlayerNow], _players, _rectangulars[_rectangularsIndex], _pass,
            Management);
        
        Log.Show();
       
    }

    public override void CloseField()
    {
        throw new NotImplementedException();
    }

    #region  Изменить сивол для прямоугольника

    private void ChangeSymbolAll()
    {
        for (var index = 0; index < _bottomLayer.SizeLayers.Height; index++)
        {
            for (var j = 0; j < _bottomLayer.SizeLayers.Width - 1; j++)
            {
                var cellOne = _bottomLayer.Matrix[index][j];
                var cellTwo = _bottomLayer.Matrix[index][j + 1];

                ChangeSymbolHorizontal(cellOne, cellTwo);
            }
        }
        
        for (var index = 0; index < _bottomLayer.SizeLayers.Width; index++)
        {
            for (var j = 0; j < _bottomLayer.SizeLayers.Height - 1; j++)
            {
                var cellOne = _bottomLayer.Matrix[j][index];
                var cellTwo = _bottomLayer.Matrix[j + 1][index];

                ChangeSymbolVertical(cellOne, cellTwo);
            }
        }
        
    }
    
    private bool ChangeSymbolHorizontal(Cell cellOne, Cell cellTwo)
    {
        // горизонтальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.VerticalLine, Mapping.VoidSquare
            )) return true;
        
        // верхнии уголы
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperRightCorner, Mapping.UpperLeftCorner, 
                Mapping.HorizontalLine, null
            )) return true;
               
        // нижние уголы
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerRightCorner, Mapping.LowerLeftCorner,
                Mapping.HorizontalLine, null
            )) return true;
                
        // Вехние правый угл и вертикальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperRightCorner, Mapping.VerticalLine,
                Mapping.HorizontalLine, Mapping.LowerRightCorner
            )) return true;

        // Вертикальная линия и нижние правый угол  
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.VerticalLine, Mapping.LowerLeftCorner,
                Mapping.UpperLeftCorner, Mapping.HorizontalLine
            )) return true;
                
        // Нижний правый угол и вертикальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerRightCorner, Mapping.VerticalLine,
                Mapping.HorizontalLine, Mapping.UpperRightCorner
            )) return true;
                
        // Вертикальная линия и верхний левый угол
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.VerticalLine, Mapping.UpperLeftCorner,
                Mapping.LowerLeftCorner, Mapping.HorizontalLine
            )) return true;
        
        // 
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperRightCorner, Mapping.VerticalLine,
                Mapping.HorizontalLine, Mapping.LowerLeftCorner
            )) return true;
        
        // 
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.VerticalLine, Mapping.LowerLeftCorner,
                Mapping.LowerRightCorner, Mapping.HorizontalLine
            )) return true;
        
        // Вехние правый угл и вертикальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperLeftCorner, Mapping.UpperRightCorner,
                Mapping.VoidSquare, null
            )) return true;
        
        // Вехние правый угл и вертикальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerLeftCorner, Mapping.LowerRightCorner,
                Mapping.VoidSquare, null
            )) return true;
        
        return false;
    }
    
    private bool ChangeSymbolVertical(Cell cellOne, Cell cellTwo)
    {
        // вертикальная линия
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.HorizontalLine, Mapping.VoidSquare
            )) return true;
        
        // нижний левый угол и вехний левый угол
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerLeftCorner, Mapping.UpperLeftCorner,
                Mapping.VerticalLine, null 
            )) return true;
        
        // нижний правый угол и верхний правый угол
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerRightCorner, Mapping.UpperRightCorner,
                Mapping.VerticalLine, null 
            )) return true;
        
        // Горизонтальная линия и Верхний правый угол
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.HorizontalLine, Mapping.UpperRightCorner, 
                Mapping.UpperLeftCorner, Mapping.VerticalLine 
            )) return true;
        
        // Горизонтальная линия и Верхний левый угол
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.HorizontalLine, Mapping.UpperLeftCorner, 
                Mapping.UpperRightCorner, Mapping.VerticalLine 
            )) return true;
        
        
        // 
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerRightCorner, Mapping.HorizontalLine,  
                 Mapping.VerticalLine, Mapping.LowerLeftCorner 
            )) return true;
        
        // 
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.LowerLeftCorner, Mapping.HorizontalLine,  
                Mapping.VerticalLine, Mapping.LowerRightCorner  
            )) return true;
        
        // 
        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperRightCorner, Mapping.LowerRightCorner,  
                Mapping.VoidSquare, null  
            )) return true;

        if (ChangeSymbol(
                cellOne, cellTwo,
                Mapping.UpperLeftCorner, Mapping.LowerLeftCorner,  
                Mapping.VoidSquare, null  
            )) return true;
        
        
        
        return false;
    }



    private bool ChangeSymbol(Cell cellOne, Cell cellTwo, 
        Mapping symbolOne, Mapping? symbolTwo,
        Mapping changeSymbolOne, Mapping? changeSymbolTwo)
    {
        
        symbolTwo ??= symbolOne;
        changeSymbolTwo ??= changeSymbolOne;
        
        if (cellOne.Responsible != cellTwo.Responsible) return false;
        if (cellOne.Display != symbolOne || cellTwo.Display != symbolTwo) return false;
        cellOne.Display = changeSymbolOne;
        cellTwo.Display = (Mapping)changeSymbolTwo;
        return true;

    }
    
    private bool ChangeSymbol(Cell cellOne, Cell cellTwo, 
        Mapping symbol, 
        Mapping changeSymbol)
    {
        return ChangeSymbol(cellOne, cellTwo, 
            symbol, symbol, 
            changeSymbol, changeSymbol);
    }

    #endregion




}
































