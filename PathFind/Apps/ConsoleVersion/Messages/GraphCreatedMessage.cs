using ConsoleVersion.Model;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Messages
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
