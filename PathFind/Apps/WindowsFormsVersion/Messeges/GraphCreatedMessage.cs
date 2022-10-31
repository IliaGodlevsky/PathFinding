using GraphLib.Realizations.Graphs;
using WindowsFormsVersion.Model;

namespace WindowsFormsVersion.Messeges
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
