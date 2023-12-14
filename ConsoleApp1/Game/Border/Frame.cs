using ConsoleApp1.Game.Border.Container;
using ConsoleApp1.Game.Border.Container.ItemContainer;
using ConsoleApp1.Game.Items.Matrix;
using ConsoleApp1.Game.Players;
using ConsoleApp1.Game.Rules.Management;

namespace ConsoleApp1.Game.Border;

using ConsoleApp1.Game.Layers;

public class Frame
{
    
    public static void ConsoleWrite(Layers layers, Player playerNow)
    {
        Console.Clear();

        var size = layers.SizeLayers + new Size(2, 2);
        
        var partsBottomLayerInfo = new ContainerLine(Alignment.Alignment.End);
        
        partsBottomLayerInfo.Add(new ContainerLinePart("Player: " + playerNow.Name, playerNow.RealColor));
        
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
        
        var lowerLayer = new BottomContainer(size);
        
        lowerLayer.Add(partsBottomLayerInfo);
        lowerLayer.Add(partsBottomLayerRules);
        

        lowerLayer.ConsoleWrite();
        

        layers.ConsoleWrite(new Shift(1, 1));
    }
 } 
 
       