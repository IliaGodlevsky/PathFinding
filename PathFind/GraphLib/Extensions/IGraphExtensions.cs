using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IGraphExtensions
    {
        public static void Refresh(this IGraph graph)
        {
            graph.RemoveExtremeVertices();

            foreach (var vertex in graph)
            {
                vertex.Refresh();
            }
        }

        internal static void RemoveExtremeVertices(this IGraph graph)
        {
            graph.End = new DefaultVertex();
            graph.Start = new DefaultVertex();
        }

        public static void ToUnweighted(this IGraph graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MakeUnweighted();
            }
        }

        public static void ToWeighted(this IGraph graph)
        {
            foreach (var vertex in graph)
            {
                vertex.MakeWeighted();
            }
        }

        internal static bool IsExtremeVerticesVisited(this IGraph self)
        {
            return self.End.IsVisited && self.Start.IsVisited
                && !self.End.IsDefault && !self.Start.IsDefault;
        }

        internal static void ConnectVertices(this IGraph self)
        {
            self.AsParallel().ForAll(vertex => vertex.SetNeighbours(self));
        }

        public static bool IsReadyForPathfinding(this IGraph self)
        {
            return !self.End.IsDefault
                && !self.Start.IsDefault
                && self.Any()
                && !self.Start.IsVisited;
        }
    }
}
