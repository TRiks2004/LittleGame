using ConsoleApp1.Game.Border.Container;
using ConsoleApp1.Game.Border.Container.ItemContainer;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Items.Rectangular;
using ConsoleApp1.Game.Players;
using ConsoleApp1.Game.Rules.Management;

namespace ConsoleApp1.Game.Border;

using ConsoleApp1.Game.Layers;

public class Frame
{
    
    public static void ConsoleWrite(LayersBase layers, Player playerNow, List<Player> players, Rectangular rectangular,
        int pass)
    {
        Console.Clear();

        
        
        var size = layers.SizeLayers + new Size(2, 2);

        #region Borders

        // Console.SetCursorPosition(0, 0);
        // Console.Write(Symbol.GetCharFromSymbol(Mapping.UpperLeftCorner));
        // for (int i = 0; i < size.Width - 2; i++)
        // {
        //     Console.Write(Symbol.GetCharFromSymbol(Mapping.HorizontalLine));
        // }
        // Console.Write(Symbol.GetCharFromSymbol(Mapping.UpperRightCorner));
        //
        // for (int i = 0; i < size.Height - 2; i++)
        // {
        //     Console.SetCursorPosition(0, i + 1);
        //     Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
        //     
        //     Console.SetCursorPosition(size.Width - 1, i + 1);
        //     Console.Write(Symbol.GetCharFromSymbol(Mapping.VerticalLine));
        // }

        #endregion
        
        #region Info

        var partsBottomLayerInfo = new ContainerLine(Alignment.Alignment.End);

        var realSize = rectangular.SizeRectangular;
        
        partsBottomLayerInfo.Add(new ContainerLinePart($"Прямоугольник: {realSize.Width - Rectangular.DefaultSizeRectangularPlus.Width} " +
                                                       $"x {realSize.Height - Rectangular.DefaultSizeRectangularPlus.Height}", ConsoleColor.DarkMagenta));
        
        partsBottomLayerInfo.Add(new ContainerLinePart($"Число пропусков: {pass}", ConsoleColor.Red));
        
        partsBottomLayerInfo.Add(new ContainerLinePart("Player: " + playerNow.Name, playerNow.RealColor));

        #endregion
        
        #region Rules

        var management = new ManagementGame();
        
        var listPartsRules = new List<ContainerLinePart>();
        foreach (var keyboardKey in management.GetKeys())
        {
            listPartsRules.Add(new ContainerLinePart(
                $"{keyboardKey.Symbol} - {keyboardKey.Destination}", 
                keyboardKey.Color));
        }
        
        var partsBottomLayerRules = new ContainerLine(listPartsRules, Alignment.Alignment.Center);

        #endregion

        #region Points

        var listPartsPoints = new List<ContainerLinePart>();
        
        foreach (var player in players)
        {
            listPartsPoints.Add(new ContainerLinePart(
                $"{player.Name}: {player.CountPoint}", player.RealColor));
        }
        
        var partsBottomLayerPoint = new ContainerLine(listPartsPoints, Alignment.Alignment.Center);

        #endregion
        
        var lowerLayer = new BottomContainer(size);
        
        lowerLayer.Add(partsBottomLayerInfo);
        lowerLayer.Add(partsBottomLayerRules);
        lowerLayer.Add(partsBottomLayerPoint);
        
        lowerLayer.ConsoleWrite();

        
        layers.ConsoleWrite(new Shift(1, 1));
        
    }
} 
 
       