namespace ConsoleApp1.Game.Rules;

public class GameRules
{
    /// <summary>
    /// Размешенные друг на друга
    /// </summary>
    public bool StirredEachOther { get; set; } = false;
    
    /// <summary>
    /// Размешенные рядом с похожим 
    /// </summary>
    public bool StirredNextSimilarOne { get; set; } = true;
    
    public GameRules()
    {
        
    }
    
    public GameRules(bool stirredEachOther, bool stirredNextSimilarOne)
    {
        StirredEachOther = stirredEachOther;
        StirredNextSimilarOne = stirredNextSimilarOne;
    }

    public override string ToString()
    {
        return $"Rules: StirredEachOther => {StirredEachOther}, StirredNextSimilarOne => {StirredNextSimilarOne}";
    }
}