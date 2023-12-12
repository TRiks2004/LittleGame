namespace ConsoleApp1;

public static class Log
{
    private static readonly List<string> Logs = new List<string>();
    
    public static void Add(string log)
    {
        if (Logs.Count == 0) AddLn(log);
        Logs[^1] += log;
    }
    
    public static void AddLn(string log)
    {
        Logs.Add(log);
    }
    
    
    public static void Show()
    {
        for (var index = 0; index < Logs.Count; index++)
        {
            Console.SetCursorPosition(120, index);
            Console.Write(Logs[index]);
        }
    }
    
    public static void Clear()
    {
        Logs.Clear();
    }
    
    
    
}