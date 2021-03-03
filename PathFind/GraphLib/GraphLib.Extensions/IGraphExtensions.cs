using Common;
using Common.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
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
            graph.Vertices.ForEach(vertex => vertex.Refresh());
        }

        public static int GetSize(this IGraph graph)
        {
            return graph.DimensionsSizes.AggregateOrDefault((x, y) => x * y);
        }

        public static int GetObstaclesCount(this IGraph graph)
        {
            return graph.Vertices.Count(vertex => vertex.IsObstacle);
        }

        public static int GetObstaclesPercent(this IGraph graph)
        {
            return graph.GetSize() == 0 ? 0 : graph.GetObstaclesCount() * 100 / graph.GetSize();
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

        public static void ConnectVerticesParallel(this IGraph self)
        {
            self.Vertices.AsParallel().ForAll(vertex => vertex.SetNeighbours(self));
        }

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualSizes = self.GetSize() == graph.GetSize();
            bool hasEqualDimensionSizes = self.DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
            bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == self.GetObstaclesCount();
            bool hasEqualVertices = self.Vertices.Match(graph.Vertices, (a, b) => a.IsEqual(b));
            return hasEqualSizes && hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static bool Contains(this IGraph self, params IVertex[] vertices)
        {
            foreach(var vertex in vertices)
            {
                if (!self.Vertices.Any(v => ReferenceEquals(v, vertex)))
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

        public static IEnumerable<int> ToCoordinates(this IGraph self, int index)
        {
            var dimensions = self.DimensionsSizes.ToArray();

            if (!dimensions.Any())
            {
                throw new ArgumentException("Dimensions count must be greater than 0");
            }

            var rangeOfValidIndexValues = new ValueRange(self.GetSize(), 0);
            if (!rangeOfValidIndexValues.IsInRange(index))
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            return Enumerable
                .Range(0, dimensions.Length)
                .Select(i => GetCoordinateValue(ref index, dimensions[i]));
        }

        private static int GetCoordinateValue(ref int index, int dimensionSize)
        {
            int coordinate = index % dimensionSize;
            index /= dimensionSize;
            return coordinate;
        }
    }
}
