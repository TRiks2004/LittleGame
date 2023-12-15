using ConsoleApp1.Game.Border;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Navigation;
using ConsoleApp1.Game.Terms.Management;
using KeyBoard;

namespace ConsoleApp1.Game.Field;

public class RulesField : Field
{
    private readonly List<List<string>> _tom = new ();
    private int _indexTom = 0;

    public RulesField(Size sizePade) : base(sizePade)
    {
        Management = new ManagementRules();
        
        Listener.OnKeyPressExit += ListenerOnOnKeyPressExit;
        Listener.OnKeyPressLeft += ListenerOnOnKeyPressLeft;
        Listener.OnKeyPressRight += ListenerOnOnKeyPressRight;
        
        Listener.StartKeyBoardListener();
        
        
        _tom.Add(new List<string>()
        {
            "Название игры: Block Out",
            "Количество игроков: 2-4",
            "Цель игры: Захватывайте территорию, размещая прямоугольники",
            "           рядом со своими, и избегайте пропуска ходов.",
        });
        
        _tom.Add(new List<string>()
        {
            "Подготовка:",
            "\t1. Каждый игрок выбирает цвет или маркер для своих прямоугольников.",
            "\t2. Игровое поле представляет собой прямоугольную сетку. Игроки начинают в углах поля.",
        });
        
        _tom.Add(new List<string>()
        {
            "Ход игры:",
            "\t1. На каждом ходу игрока генерируются два случайных размера ", 
            "\t   прямоугольника (длина и высота).",
            "\t2. Прямоугольник должен касаться хотя бы одной стороны ",
            "\t   прямоугольника того же цвета, что и он сам.",
            "\t3. Если игрок не может сделать ход (невозможно ",
            "\t   разместить прямоугольник), он пропускает свой ход.",
        });

        _tom.Add(new List<string>()
        {
            "Завершение игры:",
            "\t1. Игра продолжается до тех пор, пока все игроки не смогут сделать ход.",
        });
        
        _tom.Add(new List<string>()
        {
            "Определение победителя:",
            "\t1. Подсчитываются занимаемые каждым игроком клетки на поле.",
            "\t2. Побеждает игрок с наибольшим числом занятых клеток.",
        });
        
        
        ConsoleWrite();
    }

    private void ListenerOnOnKeyPressRight()
    {
        _indexTom++;
        if (_indexTom >= _tom.Count) _indexTom = 0;
    }

    private void ListenerOnOnKeyPressLeft()
    {
        _indexTom--;
        if (_indexTom < 0) _indexTom = _tom.Count - 1;
    }

    private void ListenerOnOnKeyPressExit()
    {
        Navigation.Navigation.NextField(NavigationMap.Start);
        CloseField();
    }
    

    public sealed override void ConsoleWrite()
    {
        Console.Clear();
        
        var tom = _tom[_indexTom];
        
        var sizeGameField = SizeGameField + new Size(1, 1);

        var strMax = tom.Max(s => s.Length);
        
        for (var index = 0; index < tom.Count; index++)
        {
            var s = tom[index];
            
            Console.SetCursorPosition(
                sizeGameField.Width / 2 - strMax / 2, 
                sizeGameField.Height / 2 - tom.Count / 2 + index
                );
            
            Console.Write(s);
        }

        var list = $"- {_indexTom + 1}/{_tom.Count} -";
        
        Console.SetCursorPosition(
            sizeGameField.Width / 2 - list.Length / 2, 
            sizeGameField.Height - 2
        );

        Console.Write(list);
        
        Frame.ConsoleWrite(SizeGameField, Management);
        
        Log.Show();
    }

    public override void CloseField()
    {
        Listener.StopKeyBoardListener();
    }
}