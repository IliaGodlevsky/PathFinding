using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;

namespace Pathfinding.App.Console.Messages
{
    internal sealed class GraphCreatedMessage
    {
        public Graph2D<Vertex> Graph { get; }

        public GraphCreatedMessage(Graph2D<Vertex> graph)
        {
            Graph = graph;
        }
    }
}
