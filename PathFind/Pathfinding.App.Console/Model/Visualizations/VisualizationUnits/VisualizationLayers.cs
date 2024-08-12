using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Service.Interface.Models.Read;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class VisualizationLayers(RunVisualizationModel algorithm)
        : Layers(new RestoreVisualStateLayer(),
            new ApplyCostsVisualizationLayer(algorithm),
            new ObstaclesVisualizationLayer(algorithm),
            new RangeVisualizationLayer(algorithm),
            new SubAlgorithmVisualizationLayer(algorithm))
    {

    }
}
