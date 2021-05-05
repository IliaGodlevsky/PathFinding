using Common;
using Common.Extensions;
using GraphLib.Interfaces;
using System;
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

        public static bool AreNeighbours(this IGraph self, IVertex first, IVertex second)
        {
            bool IsAtSamePosition(IVertex v) => v.Position.IsEqual(first.Position);
            return second.Neighbours.Any(IsAtSamePosition) && self.Contains(first, second);
        }

        public static bool CanBeNeighbourOf(this IGraph self, IVertex first, IVertex second)
        {
            return !ReferenceEquals(first, second) && !self.AreNeighbours(first, second);
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

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualDimensionSizes = self.DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
            bool hasEqualNumberOfObstacles = graph.Obstacles == self.Obstacles;
            bool hasEqualVertices = self.Vertices.Match(graph.Vertices, (a, b) => a.Equals(b));
            return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
        }

        public static void ConnectVertices(this IGraph self)
        {
            self.Vertices.ForEach(vertex => vertex.SetNeighbours(self));
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

        public static int[] ToCoordinates(this IGraph self, int index)
        {
            var dimensions = self.DimensionsSizes.ToArray();

            #region Invariant observance
            if (!dimensions.Any())
            {
                throw new ArgumentException("Dimensions count must be greater than 0");
            }

            var rangeOfValidIndices = new ValueRange(self.Size, 0);
            if (!rangeOfValidIndices.Contains(index))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            #endregion

            #region Helper methods
            int GetCoordinateValue(int dimensionSize)
            {
                int coordinate = index % dimensionSize;
                index /= dimensionSize;
                return coordinate;
            }

            int Coordinates(int i) => GetCoordinateValue(dimensions[i]);
            #endregion

            return Enumerable
                .Range(0, dimensions.Length)
                .Select(Coordinates)
                .ToArray();
        }
    }
}
