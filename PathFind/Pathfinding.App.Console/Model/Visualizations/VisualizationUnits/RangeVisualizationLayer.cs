using Pathfinding.Service.Interface.Visualization;
using Pathfinding.Visualization.Extensions;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class RangeVisualizationLayer(RunVisualizationDto algorithm)
        : VisualizationLayer(algorithm)
    {
        public override void Overlay(IGraph<IVertex> graph)
        {
            algorithm.GraphState.Range
                .Select(graph.Get)
                .OfType<ITotallyVisualizable>()
                .VisualizeAsRange();
        }
    }
}
