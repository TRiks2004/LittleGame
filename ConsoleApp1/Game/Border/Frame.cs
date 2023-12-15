using ConsoleApp1.Game.Border.Container;
using ConsoleApp1.Game.Border.Container.ItemContainer;
using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Items.Rectangular;
using ConsoleApp1.Game.Players;
using ConsoleApp1.Game.Terms.Management;

namespace ConsoleApp1.Game.Border;

using ConsoleApp1.Game.Layers;

public class Frame
{
    
    public static void ConsoleWrite(Layers layers, Player playerNow, List<Player> players, Rectangular rectangular,
        int pass, Management management)
    {
        var size = layers.SizeLayers + new Size(2, 2);
        
        #region Info

        var partsBottomLayerInfo = new ContainerLine(Alignment.Alignment.End);

        var realSize = rectangular.SizeRectangular;
        
        partsBottomLayerInfo.Add(new ContainerLinePart($"Прямоугольник: {realSize.Width - Rectangular.DefaultSizeRectangularPlus.Width} " +
                                                       $"x {realSize.Height - Rectangular.DefaultSizeRectangularPlus.Height}", ConsoleColor.DarkMagenta));
        
        partsBottomLayerInfo.Add(new ContainerLinePart($"Число пропусков: {pass}", ConsoleColor.Red));
        
        partsBottomLayerInfo.Add(new ContainerLinePart("Player: " + playerNow.Name, playerNow.RealColor));

        #endregion
        
        #region Rules
        
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

    public static void ConsoleWrite(Size layerSize, Management management)
    {
        var size = layerSize + new Size(2, 2);
        
        var listPartsRules = new List<ContainerLinePart>();
        foreach (var keyboardKey in management.GetKeys())
        {
            listPartsRules.Add(new ContainerLinePart(
                $"{keyboardKey.Symbol} - {keyboardKey.Destination}", 
                keyboardKey.Color));
        }
        
        var partsBottomLayerRules = new ContainerLine(listPartsRules, Alignment.Alignment.Center);
        
        var lowerLayer = new BottomContainer(size);
        lowerLayer.Add(partsBottomLayerRules);
        
        lowerLayer.ConsoleWrite();
        
    }
} 
 
       