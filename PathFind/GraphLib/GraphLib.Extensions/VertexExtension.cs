using Common.Extensions;
using Common.ReadOnly;
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

        public static void SetToDefault<TVertex>(this TVertex self)
            where TVertex : IVertex, IVisualizable
        {
            if (self.IsObstacle)
            {
                self.VisualizeAsObstacle();
            }
            else
            {
                self.VisualizeAsRegular();
            }
        }

        public static bool IsCardinal(this IVertex vertex, IVertex neighbor)
        {
            return vertex.Position.IsCardinal(neighbor.Position);
        }

        public static IVertex AsVertex(this object self)
        {
            return self.As<IVertex>(NullVertex.Interface);
        }

        public static void Initialize<TVertex>(this TVertex self)
            where TVertex : IVertex, IVisualizable
        {
            (self as IVertex).Initialize();
            self.SetToDefault();
        }

        public static void Initialize(this IVertex self)
        {
            self.IsObstacle = false;
            self.Cost = NullCost.Interface;
            self.Neighbours = ReadOnlyList<IVertex>.Empty;
        }

        internal static void Refresh<TVertex>(this TVertex self)
            where TVertex : IVertex, IVisualizable
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