using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.WPF._2D.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory<Vertex, GraphField>
    {
        public GraphField CreateGraphField(IGraph<Vertex> graph)
        {
            return new GraphField(graph);
        }
    }
}
