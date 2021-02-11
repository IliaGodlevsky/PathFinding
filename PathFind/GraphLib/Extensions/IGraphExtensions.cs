using Common.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class IGraphExtensions
    {
        /// <summary>
        /// Removes all actions, that was performed over the vertices
        /// </summary>
        /// <param name="graph"></param>
        public static void Refresh(this IGraph graph)
        {
            graph.ForEach(vertex => vertex.Refresh());
        }

        public static int GetSize(this IGraph graph)
        {
            return graph.DimensionsSizes.AggregateOrDefault((x, y) => x * y);
        }

        public static int GetObstaclesCount(this IGraph graph)
        {
            return graph.Count(vertex => vertex.IsObstacle);
        }

        public static int GetObstaclesPercent(this IGraph graph)
        {
            return graph.GetSize() == 0 ? 0 : graph.GetObstaclesCount() * 100 / graph.GetSize();
        }

        public static GraphSerializationInfo GetGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }

        public static void ToUnweighted(this IGraph graph)
        {
            graph.ForEach(vertex => vertex.MakeUnweighted());
        }

        public static void ToWeighted(this IGraph graph)
        {
            graph.ForEach(vertex => vertex.MakeWeighted());
        }

        public static void ConnectVertices(this IGraph self)
        {
            self.AsParallel().ForAll(vertex => vertex.SetNeighbours(self));
        }

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualSizes = self.GetSize() == graph.GetSize();
            bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == self.GetObstaclesCount();
            bool hasEqualVertices = self.Match(graph, (a, b) => a.IsEqual(b));
            return hasEqualSizes && hasEqualNumberOfObstacles && hasEqualVertices;
        }

        public static bool Contains(this IGraph self, params IVertex[] vertices)
        {
            foreach(var vertex in vertices)
            {
                if (!self.Any(v => ReferenceEquals(v, vertex)))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Contains(this IGraph self, IEndPoints endPoints)
        {
            return self.Contains(endPoints.Start, endPoints.End);
        }
    }
}
