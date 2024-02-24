using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Realizations;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class VisualizationLayers(RunVisualizationDto algorithm) 
        : Layers(new RestoreVisualStateLayer(),
            new ApplyCostsVisualizationLayer(algorithm),
            new ObstaclesVisualizationLayer(algorithm),
            new RangeVisualizationLayer(algorithm),
            new SubAlgorithmVisualizationLayer(algorithm))
    {

    }
}
