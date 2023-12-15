using ConsoleApp1.Game.Border;
using ConsoleApp1.Game.Items.Cells;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Layers;
using ConsoleApp1.Game.Navigation;
using ConsoleApp1.Game.Terms.Management;
using ConsoleApp1.Game.Terms.Rules;
using KeyBoard;

namespace ConsoleApp1.Game.Field;

class TextSymbol
{
    public delegate void Move();
    public event Move? OnSelected;
    
    public List<string> Text { get; set; }

    public bool IsSelected { get; set; }

    public TextSymbol(List<string> text, bool selected = false)
    {
        Text = text;
        IsSelected = selected;
    }
    
    
    private string GetSelectedSymbolStart(int row)
    {
        var symbol = "";
        
        for (int i = 0; i < Text.Count / 2; i++)
        {
            if (i == row) symbol += "\\ ";
            else if (Text.Count - i - 1 == row) symbol += "/ ";
            else symbol += "  ";
        }
        
        return symbol;
    }
    
    private string GetSelectedSymbolEnd(int row)
    {
        var symbol = "";
        
        for (int i = 0; i < Text.Count / 2; i++)
        {
            if (Text.Count / 2 - i - 1 == row) symbol += " /";
            else if (i + Text.Count / 2  == row) symbol += " \\";
            else symbol += "  ";
        }
        
        return symbol;
    }
    
    private string GetSelectedRowText(int row)
    {
        return GetSelectedSymbolStart(row) + Text[row] + GetSelectedSymbolEnd(row);
    }
    
    
    public List<string> GetText()
    {
        if (IsSelected)
        {
            var text = new List<string>();

            for (var i = 0; i < Text.Count; i++)
            {
                text.Add(GetSelectedRowText(i));
            }

            return text;
        }
        return Text;
    }


    public void Selected()
    {
        OnSelected?.Invoke();
    }
}


public class StartField : Field 
{
    private readonly TextSymbol _newGame;
    private readonly TextSymbol _rules;
    private readonly TextSymbol _exit;
    
    private readonly List<TextSymbol> _textSymbols;
    
    private int _selectedRow = 0;

    public StartField(Size sizeGameField): base(sizeGameField)
    {
        Listener.OnKeyPress += ConsoleWrite;
        
        Listener.OnKeyPressUp += KeyBordOnOnKeyPressUp;
        Listener.OnKeyPressDown += KeyBordOnOnKeyPressDown;
        
        Listener.OnKeyPressEnter += KeyBordOnOnKeyPressEnter;

        Listener.StartKeyBoardListener();

        
        
        _newGame = new TextSymbol(new List<string>()
        {
            "╔╗╔╗╔══╗╔══╗ ╔══╗╔═══╗   ╔╗╔╗╔══╗╔═══╗╔══╗",
            "║║║║║╔╗║║╔╗║ ║╔╗║║╔═╗║   ║║║║║╔═╝║╔═╗║║╔╗║",
            "║╚╝║║║║║║╚╝╚╗║╚╝║║╚═╝║   ║║║║║║  ║╚═╝║║╚╝║",
            "║╔╗║║║║║║╔═╗║║╔╗║╚╗╔╗║   ║║╔║║║  ║╔══╝║╔╗║",
            "║║║║║╚╝║║╚═╝║║║║║ ║║║║   ║╚╝║║║  ║║   ║║║║",
            "╚╝╚╝╚══╝╚═══╝╚╝╚╝ ╚╝╚╝   ╚══╝╚╝  ╚╝   ╚╝╚╝",
        }, true);

        _rules = new TextSymbol(new List<string>()
        {

            "╔══╗╔═══╗╔══╗╔══╗ ╔╗╔╗ ╔══╗╔══╗",
            "║╔╗║║╔═╗║║╔╗║║╔╗║ ║║║║ ║╔╗║║╔╗║",
            "║║║║║╚═╝║║╚╝║║╚╝╚╗║║║║ ║║║║║╚╝║",
            "║║║║║╔══╝║╔╗║║╔═╗║║║╔║ ║║║║║╔╗║",
            "║║║║║║   ║║║║║╚═╝║║╚╝║╔╝║║║║║║║",
            "╚╝╚╝╚╝   ╚╝╚╝╚═══╝╚══╝╚═╝╚╝╚╝╚╝",
        });

        _exit = new TextSymbol(new List<string>()
        {
            "╔══╗ ╔╗  ╔╗╔══╗╔══╗╔══╗ ╔══╗ ",
            "║╔╗║ ║║  ║║╚═╗║║╔═╝║╔╗║ ║╔╗║ ",
            "║╚╝╚╗║╚══╣║  ║╚╝║  ║║║║ ║║║║ ",
            "║╔═╗║║╔═╗║║  ║╔╗║  ║║║║ ║║║║ ",
            "║╚═╝║║╚═╝║║╔═╝║║╚═╗║╚╝║╔╝╚╝╚╗",
            "╚═══╝╚═══╩╝╚══╝╚══╝╚══╝╚════╝",
        });
        
        _textSymbols = new List<TextSymbol>{_newGame, _rules, _exit};
        
        _newGame.OnSelected += NewGameOnOnSelected;
        _rules.OnSelected += RulesOnOnSelected;
        _exit.OnSelected += ExitOnOnSelected;
        
        ConsoleWrite();
    }

    private void ExitOnOnSelected()
    {
        Environment.Exit(0);
    }

    private void RulesOnOnSelected()
    {
        Navigation.Navigation.NextField(NavigationMap.Rules);
        CloseField();
    }

    private void NewGameOnOnSelected()
    {
        Navigation.Navigation.NextField(NavigationMap.Game);
        CloseField();
    }


    private void KeyBordOnOnKeyPressEnter()
    {
        foreach (var textSymbol in _textSymbols)
        {
            if (textSymbol.IsSelected) textSymbol.Selected();
        }
    }

    private void KeyBordOnOnKeyPressDown()
    {
        _textSymbols[_selectedRow].IsSelected = false;
        
        _selectedRow++;
        if (_selectedRow >= _textSymbols.Count)
        {
            _selectedRow = 0;
        }
        
        _textSymbols[_selectedRow].IsSelected = true;
    }
    

    private void KeyBordOnOnKeyPressUp()
    {
        _textSymbols[_selectedRow].IsSelected = false;
        
        _selectedRow--;
        if (_selectedRow == -1)
        {
            _selectedRow = _textSymbols.Count - 1;
        }
        
        _textSymbols[_selectedRow].IsSelected = true;
        
    }


    public sealed override void ConsoleWrite()
    {
        Console.Clear();

        var newSize = SizeGameField + new Size(1, 1);
        
        var height = _textSymbols.Sum(symbol => symbol.Text.Count) + (_textSymbols.Count - 1);

        var top = (newSize.Height / 2) - (height / 2);;
        
        for (var index = 0; index < _textSymbols.Count; index++)
        {
            var textSymbol = _textSymbols[index].GetText();

            for (var j = 0; j < textSymbol.Count; j++)
            {
                var s = textSymbol[j];
                
                var left = (newSize.Width / 2) - (s.Length / 2);
                
                
                Console.SetCursorPosition(
                    left, 
                    textSymbol.Count * index + j + top + (index != 0? index : 0));
                Console.Write(s);
            }
        }

        
        

        Frame.ConsoleWrite(SizeGameField, new ManagementStart());
        
        Log.Show();
    }

    public override void CloseField()
    {
        Listener.StopKeyBoardListener();
    }
}