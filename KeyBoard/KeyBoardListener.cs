namespace KeyBoard;

public delegate void KeyPresEvent();

class EventDictionary
{
    public ConsoleKey Key{ get; set; }
    public  KeyPresEvent? Event{ get; set; }
    
    public EventDictionary(ConsoleKey key, KeyPresEvent? presEvent)
    {
        Key = key;
        Event = presEvent;
    }
}

public class KeyBoardListener
{
    public event KeyPresEvent? OnKeyPress;

    public event KeyPresEvent? OnKeyPressUp;
    public event KeyPresEvent? OnKeyPressDown;
    public event KeyPresEvent? OnKeyPressRight;
    public event KeyPresEvent? OnKeyPressLeft;
    
    public event KeyPresEvent? OnKeyPressEnter;
    public event KeyPresEvent? OnKeyPressSpace;
    
    public event KeyPresEvent? OnKeyPressExit;
    
    private bool _isKeyBoardListenerRunning;

    public KeyBoardListener() { }
    
    public void StartKeyBoardListener()
    {

        _isKeyBoardListenerRunning = true;
        new Task(() =>
        {
            while (_isKeyBoardListenerRunning)
            {
                if (!Console.KeyAvailable) continue;
                var key = Console.ReadKey(intercept: true);
                
                if      (key.Key == ButtonControl.Up)      OnExecutionEvent(OnKeyPressUp);
                else if (key.Key == ButtonControl.Down)    OnExecutionEvent(OnKeyPressDown);
                else if (key.Key == ButtonControl.Left)    OnExecutionEvent(OnKeyPressLeft);
                else if (key.Key == ButtonControl.Right)   OnExecutionEvent(OnKeyPressRight);
                else if (key.Key == ButtonControl.Enter)   OnExecutionEvent(OnKeyPressEnter);
                else if (key.Key == ButtonControl.Space)   OnExecutionEvent(OnKeyPressSpace);
                else if (key.Key == ButtonControl.Escape)  OnExecutionEvent(OnKeyPressExit);
            }
            
        }).Start();
    }
    
    public void StopKeyBoardListener()
    {
        _isKeyBoardListenerRunning = false;
        ClearEvent();
    }
    
    private void OnExecutionEvent(KeyPresEvent? eventHandler)
    {
        if (eventHandler == null) return;
        eventHandler();
        OnKeyPress?.Invoke();
    }
    
    public void ClearEvent()
    {
        OnKeyPress = null;
        
        OnKeyPressUp = null;
        OnKeyPressDown = null;
        OnKeyPressRight = null;
        OnKeyPressLeft = null;
        
        OnKeyPressEnter = null;
        OnKeyPressSpace = null;
        
        OnKeyPressExit = null;
    }
}