using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.Visualization.Extensions;

namespace Pathfinding.App.Console.Model.Visualizations.VisualizationUnits
{
    internal sealed class RestoreVisualStateLayer : ILayer
    {
        public void Overlay(IGraph<IVertex> graph)
        {
            foreach (Vertex vertex in graph)
            {
                vertex.RestoreDefaultVisualState();
            }
        }
    }
}
