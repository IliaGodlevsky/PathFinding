using Pathfinding.App.Console.DAL.Models.TransferObjects.Read;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class VisualizationLayers(RunVisualizationDto algorithm) : ILayer
    {
        private readonly IEnumerable<ILayer> layers = new ILayer[]
            {   // Order sensitive
                new RestoreVisualStateLayer(),
                new ApplyCostsVisualizationLayer(algorithm),
                new ObstaclesVisualizationLayer(algorithm),
                new RangeVisualizationLayer(algorithm),
                new SubAlgorithmVisualizationLayer(algorithm)
            }.ToReadOnly();

        public void Overlay(IGraph<IVertex> graph)
        {
            layers.ForEach(x => x.Overlay(graph));
        }
    }
}
