using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;

namespace GraphLib.Extensions
{
    public static class IGraphExtensions
    {
        public static void Refresh(this IGraph graph)
        {
            graph.RemoveExtremeVertices();
            foreach (var vertex in graph)
                vertex.Refresh();
        }

        internal static void RemoveExtremeVertices(this IGraph graph)
        {
            graph.End = new DefaultVertex();
            graph.Start = new DefaultVertex();
        }

        public static void ToUnweighted(this IGraph graph)
        {
            foreach (var vertex in graph)
                vertex.MakeUnweighted();
        }

        public static void ToWeighted(this IGraph graph)
        {
            foreach (var vertex in graph)
                vertex.MakeWeighted();
        }
    }
}
