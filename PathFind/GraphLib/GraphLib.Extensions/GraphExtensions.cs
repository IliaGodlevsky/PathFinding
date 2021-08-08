using Common.Extensions;
using Common.ValueRanges;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class GraphExtensions
    {
        public static void Refresh(this IGraph graph)
        {
            graph.ForEach(vertex => vertex.Refresh());
        }

        public static void ToUnweighted(this IGraph graph)
        {
            graph.Vertices
                .OfType<IWeightable>()
                .ForEach(vertex => vertex.MakeUnweighted());
        }

        public static void ToWeighted(this IGraph graph)
        {
            graph.Vertices
                .OfType<IWeightable>()
                .ForEach(vertex => vertex.MakeWeighted());
        }

        public static bool IsEmpty(this IGraph self)
        {
            return !self.Vertices.Any();
        }

        public static bool HasVertices(this IGraph self)
        {
            return !self.IsEmpty();
        }

        public static int GetObstaclePercent(this IGraph self)
        {
            return self.Size == 0 ? 0
                : self.GetObstaclesCount() * 100 / self.Size;
        }

        public static int GetObstaclesCount(this IGraph self)
        {
            return self.Vertices.Count(vertex => vertex.IsObstacle);
        }

        public static IGraph ForEach(this IGraph self, Action<IVertex> action)
        {
            self.Vertices.ForEach(action);
            return self;
        }

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualDimensionSizes = self.DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
            bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == self.GetObstaclesCount();
            bool hasEqualVertices = self.Vertices.Match(graph.Vertices, (a, b) => a.Equals(b));
            return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static IGraph ConnectVertices(this IGraph self)
        {
            self.ForEach(vertex => vertex.SetNeighbours(self));
            return self;
        }

        public static bool Contains(this IGraph self, params IVertex[] vertices)
        {
            return self.Vertices.ContainsElems(vertices);
        }

        public static bool Contains(this IGraph self, IEndPoints endPoints)
        {
            return self.Contains(endPoints.Source, endPoints.Target);
        }

        public static IVertex[] GetNotObstacles(this IGraph self)
        {
            return self.Vertices.FilterObstacles();
        }

        public static int[] ToCoordinates(this IGraph self, int index)
        {
            var rangeOfIndices = new InclusiveValueRange<int>(self.Size - 1, 0);
            if (!rangeOfIndices.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var dimensions = self.DimensionsSizes;
            var coordinates = new int[dimensions.Length];

            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = index % dimensions[i];
                index /= dimensions[i];
            }

            return coordinates;
        }
    }
}
