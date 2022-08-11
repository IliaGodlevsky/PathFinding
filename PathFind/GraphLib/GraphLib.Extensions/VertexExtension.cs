using Common.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using NullObject.Extensions;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class VertexExtension
    {
        public static bool IsIsolated(this IVertex self)
        {
            bool IsObstacleOrNull(IVertex vertex)
            {
                return vertex.IsNull() || vertex.IsObstacle;
            }

            return IsObstacleOrNull(self) || self.Neighbours.All(IsObstacleOrNull);
        }

        public static bool IsNeighbour(this IVertex self, IVertex candidate)
        {
            return self.Neighbours.Any(vertex
                => ReferenceEquals(vertex, candidate) && ReferenceEquals(vertex.Graph, candidate.Graph));
        }

        public static void SetToDefault(this IVertex self)
        {
            if (self.IsObstacle)
            {
                self.AsVisualizable().VisualizeAsObstacle();
            }
            else
            {
                self.AsVisualizable().VisualizeAsRegular();
            }
        }

        public static bool IsCardinal(this IVertex vertex, IVertex neighbor)
        {
            return vertex.Position.IsCardinal(neighbor.Position);
        }

        public static IVisualizable AsVisualizable(this IVertex vertex)
        {
            return vertex.As<IVisualizable>(NullVisualizable.Instance);
        }

        public static IVertex AsVertex(this object self)
        {
            return self.As<IVertex>(NullVertex.Instance);
        }

        public static void Initialize(this IVertex self)
        {
            self.IsObstacle = false;
            self.Cost = NullCost.Instance;
            self.SetToDefault();
        }

        internal static void Refresh(this IVertex self)
        {
            self.SetToDefault();
        }

        public static bool IsEqual(this IVertex self, IVertex vertex)
        {
            bool hasEqualCost = self.Cost.Equals(vertex.Cost);
            bool hasEqualPosition = self.Position.Equals(vertex.Position);
            bool hasSameObstacleStatus = self.IsObstacle == vertex.IsObstacle;
            return hasEqualCost && hasEqualPosition && hasSameObstacleStatus;
        }

        public static bool IsOneOf(this IVertex self, params IVertex[] vertices)
        {
            return vertices.Any(vertex => vertex.Equals(self));
        }
    }
}