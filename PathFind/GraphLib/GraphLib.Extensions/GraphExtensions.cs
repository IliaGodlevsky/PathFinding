using Common;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    public static class GraphExtensions
    {
        /// <summary>
        /// Removes all actions, that was performed over the vertices
        /// </summary>
        /// <param name="graph"></param>
        public static void Refresh(this IGraph graph)
        {
            graph.Vertices.ForEach(vertex => vertex.Refresh());
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

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualDimensionSizes = self.DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
            bool hasEqualNumberOfObstacles = graph.Obstacles == self.Obstacles;
            bool hasEqualVertices = self.Vertices.Match(graph.Vertices, (a, b) => a.Equals(b));
            return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static void ConnectVerticesParallel(this IGraph self)
        {
            self.Vertices.AsParallel().ForAll(vertex => vertex.SetNeighbours(self));
        }

        public static bool Contains(this IGraph self, params IVertex[] vertices)
        {
            bool IsInGraph(IVertex vertex)
            {
                return self.Vertices.Any(v => ReferenceEquals(v, vertex));
            }

            return vertices.All(IsInGraph);
        }

        public static bool Contains(this IGraph self, IEndPoints endPoints)
        {
            return self.Contains(endPoints.Start, endPoints.End);
        }

        public static IEnumerable<int> ToCoordinates(this IGraph self, int index)
        {
            var dimensions = self.DimensionsSizes.ToArray();

            if (!dimensions.Any())
            {
                throw new ArgumentException("Dimensions count must be greater than 0");
            }

            var rangeOfValidIndexValues = new ValueRange(self.Size, 0);
            if (!rangeOfValidIndexValues.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            int Coordinates(int i) => GetCoordinateValue(ref index, dimensions[i]);
            return Enumerable.Range(0, dimensions.Length).Select(Coordinates);
        }

        private static int GetCoordinateValue(ref int index, int dimensionSize)
        {
            int coordinate = index % dimensionSize;
            index /= dimensionSize;
            return coordinate;
        }
    }
}
