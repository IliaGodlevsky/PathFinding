using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace ConsoleVersion.Messages
{
    internal sealed class GraphCreatedMessage
    {
        public Graph2D Graph { get; }

        public GraphCreatedMessage(IGraph graph)
        {
            Graph = (Graph2D)graph;
        }
    }
}
