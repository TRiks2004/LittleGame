using ConsoleApp1.Game.Items.Display;
using ConsoleApp1.Game.Items.Matrix;

namespace ConsoleApp1.Game.Layers;

public class LayersBase : Layers
{
    public LayersBase(Size size) : base(size) { }
    
    public static LayersBase operator +(LayersBase topLayer, LayersBase bottomLayer)
    {
        if(topLayer.SizeLayers != bottomLayer.SizeLayers) throw new Exception("Layers must be the same size");
        
        var layersBase = new LayersBase(topLayer.SizeLayers);

        var t = 0;
        for (var i = 0; i < topLayer.SizeLayers.Height; i++)
        {
            for (var j = 0; j < topLayer.SizeLayers.Width; j++)
            {
                if (topLayer.Matrix[i][j].Display == Mapping.Void)
                {
                   
                    layersBase.Matrix[i][j] = bottomLayer.Matrix[i][j];
                }
                else
                {
                    layersBase.Matrix[i][j] = topLayer.Matrix[i][j];
                }
            }
        }


        return layersBase;
    }

    
}