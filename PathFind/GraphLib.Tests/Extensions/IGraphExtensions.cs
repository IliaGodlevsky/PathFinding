using GraphLib.Graphs.Abstractions;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Tests.Extensions
{
    public static class IGraphExtensions
    {
        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            IEqualityComparer<IVertex> comparer = new VertexComparer();
            bool hasEqualSizes = self.Size == graph.Size;
            bool hasEqualNumberOfObstacles = graph.ObstacleNumber == self.ObstacleNumber;
            bool hasEqualVertices = self.SequenceEqual(graph, comparer);
            return hasEqualSizes && hasEqualNumberOfObstacles && hasEqualVertices;
        }
    }

    class VertexComparer : IEqualityComparer<IVertex>
    {
        public bool Equals(IVertex x, IVertex y)
        {
            return x.IsEqual(y);
        }

        public int GetHashCode(IVertex obj)
        {
            var coordinatesHashCode = obj.Position.CoordinatesValues.Aggregate((x, y) => x ^ y);
            var obstacleHasCode = obj.IsObstacle.GetHashCode();
            var costHasCode = ((int)obj.Cost).GetHashCode();
            return coordinatesHashCode ^ obstacleHasCode ^ costHasCode;
        }
    }
}
