using Pathfinding.App.Console.Interface;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.Visualization.Extensions;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class RestoreVisualStateUnit : IVisualizationUnit
    {
        public void Visualize(IGraph<Vertex> graph)
        {
            graph.RestoreVerticesVisualState();
        }
    }
}
