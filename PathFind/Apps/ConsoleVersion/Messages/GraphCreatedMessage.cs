using GraphLib.Interfaces;

namespace ConsoleVersion.Messages
{
    internal sealed class GraphCreatedMessage
    {
        public IGraph Graph { get; }

        public GraphCreatedMessage(IGraph graph)
        {
            Graph = graph;
        }
    }
}
