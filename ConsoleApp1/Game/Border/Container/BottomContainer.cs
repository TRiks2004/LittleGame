using ConsoleApp1.Game.Border.Container.ItemContainer;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;

namespace ConsoleApp1.Game.Border.Container;

public class BottomContainer
{
    private List<List<int>> _lines = new List<List<int>>();

    private List<ContainerLine> PartsBottomLayer { get; set; }
    public Size SizeContainer { get; set; }

    private readonly int _shift;
    
    public BottomContainer(Size size)
    {
        PartsBottomLayer = new List<ContainerLine>();
        SizeContainer = size;
        _shift = size.Height - 1;
    }
    
    public BottomContainer(Size size, List<ContainerLine> parts): this(size)
    {
        PartsBottomLayer = parts;

        foreach (var part in PartsBottomLayer)
        {
            _lines.Add(new List<int>());
            for (int i = 0; i < SizeContainer.Width; i++)
            {
                _lines[^1].Add(0);
            }
        }
    }
    
    public void Add(ContainerLine part)
    {
        PartsBottomLayer.Add(part);
        _lines.Add(new List<int>());
        for (int i = 0; i < SizeContainer.Width; i++)
        {
            _lines[^1].Add(0);
        }
    }
    
    #region ConsoleWriteHelpers

    private void ConsoleWriteContentJoin(ContainerLine part, int start = 0, int index = 0, bool closeStart = false, bool closeEnd = false)
    {
        var newIndex = index * 2 + 1 + _shift; 
        
        Console.SetCursorPosition(0, newIndex);
        Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
        
        Console.SetCursorPosition(SizeContainer.Width - 1, newIndex);
        Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
        
        _lines[index][0] = 2;
        _lines[index][SizeContainer.Width - 1] = 3;

        if (PartsBottomLayer.Count - 1 == index)
        {
            _lines[^1][0] = 2;
            _lines[^1][SizeContainer.Width - 1] = 3;
        }
        
        
        var indexVertical = start;
        
        Console.SetCursorPosition(start, newIndex);

        for (var i = 0; i <= part.Parts.Count; i++)
        {
            if (closeStart && i == 0)
            {
                _lines[index][indexVertical] = 1;
                
                if (PartsBottomLayer.Count - 1 == index)
                {
                    _lines[^1][indexVertical] = 1;
                }
                
                indexVertical += 1;
                
                Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
            }
            
            if (closeEnd && i == part.Parts.Count)
            {
                _lines[index][indexVertical] = 1;
                if (PartsBottomLayer.Count - 1 == index)
                {
                    _lines[^1][indexVertical] = 1;
                    
                }
                
                indexVertical += 1;
                
                Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
                continue;
            }

            if (i == part.Parts.Count)
            {
                continue;
            }

            if (i != 0 )
            {
                _lines[index][indexVertical] = 1;
                if (PartsBottomLayer.Count - 1 == index)
                {
                    _lines[^1][indexVertical] = 1;
                }
                indexVertical += 1;
                
                Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
            }
            
            var partPart = part.Parts[i];
            
            Console.ForegroundColor = partPart.Color;
            
            Console.Write($" {partPart.Text} ");

            Console.ForegroundColor = ConsoleColor.White;

            indexVertical += partPart.TextLength + 2;
        }
    }
    
    private void ConsoleWriteContent(ContainerLine part, int start = 0, int index = 0, bool closeStart = false, bool closeEnd = false)
    {
        ConsoleWriteContentJoin(part, start, index, closeStart, closeEnd);
        Console.WriteLine();
    }

    private void ConsoleWriteAlignmentStart(ContainerLine part, int index)
    {
        ConsoleWriteContent(part, 1, index, false, true);
    }

    private void ConsoleWriteAlignmentCenter(ContainerLine part, int index)
    {
        var sizeContainerWidth = 
            SizeContainer.Width - (
                part.GetTextLength() + part.Parts.Count * 2 + part.Parts.Count
            );
        
        ConsoleWriteContent(part, sizeContainerWidth / 2, index, true, true);
    }

    private void ConsoleWriteAlignmentEnd(ContainerLine part, int index)
    {
        var sizeContainerWidth = 
            SizeContainer.Width - (
                part.GetTextLength() + part.Parts.Count * 2 + part.Parts.Count
            ) - 1;
        ConsoleWriteContent(part, sizeContainerWidth, index, true, false);
    }
    

    private void ConsoleWriteAlignment(ContainerLine part, Alignment.Alignment alignment, int index)
    {
        switch (alignment)
        {
            case Alignment.Alignment.Start: ConsoleWriteAlignmentStart(part, index); break;
            case Alignment.Alignment.Center: ConsoleWriteAlignmentCenter(part, index); break;
            case Alignment.Alignment.End: ConsoleWriteAlignmentEnd(part, index); break;
            case Alignment.Alignment.SpaceBetween: break;
            case Alignment.Alignment.SpaceAround: break;
            case Alignment.Alignment.SpaceEvenly: break;
            default: throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
        }    
    }
    
    #endregion
    
    public void ConsoleWrite()
    {
        _lines.Add(new List<int>());
        for (int i = 0; i < SizeContainer.Width; i++)
        {
            _lines[^1].Add(0);
        }
        
        
        for (var index = 0; index < PartsBottomLayer.Count; index++)
        {
            var part = PartsBottomLayer[index];
            ConsoleWriteAlignment(part, part.AlignmentContainer, index);
        }
        
        for (var i = 0; i < _lines.Count; i++)
        {
            Console.SetCursorPosition(0, i * 2 + _shift);
            
            var line = _lines[i];
            for (var j = 0; j < line.Count; j++)
            {
                if (line[j] == 2 && i != _lines.Count - 1)
                    Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalCenterRight));
                else if (line[j] == 3 && i != _lines.Count - 1)
                    Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalCenterLeft));
                else if (i == 0)
                {
                    if (line[j] == 1) Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterDown));
                    else Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontalLine));
                }
                else if (i == _lines.Count - 1)
                {
                    if (line[j] == 2)
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.LowerLeftCorner));
                    else if (line[j] == 3)
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.LowerRightCorner));
                    else if (line[j] == 1) 
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterUp));
                    else 
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontalLine));
                }
                else
                {
                    if (line[j] == 1 && _lines[i-1][j] == 1)
                    {
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticallyAndHorizontally));
                    }
                    else if (_lines[i - 1][j] == 1)
                    {
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterUp));
                    }
                    else if (line[j] == 1)
                    {
                        Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontallyCenterDown));
                    }
                    else Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontalLine));
                   
                }
                
                
                
            }
        }
        
    }
    
    
}