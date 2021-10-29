using Common.Extensions;
using Common.ValueRanges;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
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
            return self.Vertices.Where(vertex => vertex.IsObstacle).Count();
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
            bool hasEqualVertices = self.Vertices.Juxtapose(graph.Vertices, (a, b) => a.Equals(b));
            return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static IVertex FirstOrNullVertex(this IGraph graph)
        {
            return graph.Vertices.FirstOrDefault() ?? new NullVertex();
        }

        public static IVertex LastOrNullVertex(this IGraph graph)
        {
            return graph.Vertices.LastOrDefault() ?? new NullVertex();
        }

        public static IGraph ConnectVertices(this IGraph self)
        {
            return self.ForEach(vertex => vertex.SetNeighbours(self));
        }

        public static bool ContainsReferences(this IGraph self, params IVertex[] vertices)
        {
            return self.Vertices.ContainsReferences(vertices);
        }

        public static bool ContainsReferences(this IGraph self, IEndPoints endPoints)
        {
            return self.ContainsReferences(endPoints.Source, endPoints.Target);
        }

        public static bool ContainsReferences(this IGraph self, IIntermediateEndPoints endPoints)
        {
            return self.ContainsReferences((IEndPoints)endPoints) && self.ContainsReferences(endPoints.IntermediateVertices.ToArray());
        }

        public static int GetIsolatedCount(this IGraph self)
        {
            return self.Vertices.Where(vertex => vertex.IsIsolated()).Count();
        }

        public static IEnumerable<IVertex> GetNotObstacles(this IGraph self)
        {
            return self.Vertices.FilterObstacles();
        }

        /// <summary>
        /// Converts <paramref name="index"/> into an array of 
        /// cartesian coordinates according to graph dimension sizes
        /// </summary>
        /// <param name="self"></param>
        /// <param name="index"></param>
        /// <returns>An array of cartesian coordinates</returns>
        public static int[] ToCoordinates(this int[] dimensionSizes, int index)
        {
            int size = dimensionSizes.AggregateOrDefault(IntExtensions.Multiply);
            var rangeOfIndices = new InclusiveValueRange<int>(size - 1, 0);
            if (!rangeOfIndices.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var coordinates = new int[dimensionSizes.Length];

            for (int i = 0; i < coordinates.Length; i++)
            {
                coordinates[i] = index % dimensionSizes[i];
                index /= dimensionSizes[i];
            }

            return coordinates;
        }
    }
}