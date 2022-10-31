using GraphLib.Realizations.Graphs;
using WPFVersion.Model;

namespace WPFVersion.Messages.DataMessages
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
