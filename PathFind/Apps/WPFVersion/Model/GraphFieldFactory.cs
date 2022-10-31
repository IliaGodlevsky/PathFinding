using GraphLib.Interfaces;
using GraphLib.Realizations.Graphs;

namespace WPFVersion.Model
{
    internal sealed class GraphFieldFactory : IGraphFieldFactory<Graph2D<Vertex>, Vertex, GraphField>
    {
        public GraphField CreateGraphField(Graph2D<Vertex> graph)
        {
            return new GraphField(graph);
        }
    }
}
