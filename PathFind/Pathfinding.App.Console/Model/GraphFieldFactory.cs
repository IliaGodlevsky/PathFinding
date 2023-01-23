using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.VisualizationLib.Core.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>
    {
        public GraphField CreateGraphField(Graph2D<Vertex> graph)
        {
            return new(graph);
        }
    }
}
