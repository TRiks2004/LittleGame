namespace ConsoleApp1.Game.Items.SealBone;

public class SealBone
{
    // Игральная кость

    private int _numberFaces;
    private Random _random = new Random();
    
    public SealBone(int numberFaces)
    {
        _numberFaces = numberFaces;
    }

    public int Throw()
    {
        return _random.Next(1, _numberFaces + 1);
    }
}