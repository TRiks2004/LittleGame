using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Terms.Management;
using KeyBoard;

namespace ConsoleApp1.Game.Field;

public abstract class Field
{
    protected Management Management;
    protected Size SizeGameField;
    protected KeyBoardListener Listener;

    protected Field(Size sizeGameField)
    {
        SizeGameField = sizeGameField;
        
        Listener = new KeyBoardListener();
        Listener.OnKeyPress += ConsoleWrite;
    }

    public abstract void ConsoleWrite();
    
    public abstract void CloseField();

}