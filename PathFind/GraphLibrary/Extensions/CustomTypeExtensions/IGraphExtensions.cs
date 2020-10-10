using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex;
using System.Linq;

namespace GraphLibrary.Extensions.CustomTypeExtensions
{
    public static class IGraphExtensions
    {
        public static void Refresh(this IGraph graph)
        {
            graph.RemoveExtremeVertices();
            foreach (var vertex in graph)
                vertex.Refresh();
        }

        public static void RemoveExtremeVertices(this IGraph graph)
        {
            graph.End = NullVertex.Instance;
            graph.Start = NullVertex.Instance;
        }
    }
}
