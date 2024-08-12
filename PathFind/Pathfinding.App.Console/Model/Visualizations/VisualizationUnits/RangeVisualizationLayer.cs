using Pathfinding.Domain.Interface;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Service.Interface.Models.Read;
using Pathfinding.Service.Interface.Visualization;
using System.Linq;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class RangeVisualizationLayer(RunVisualizationModel algorithm)
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
