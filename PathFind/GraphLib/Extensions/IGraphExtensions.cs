using Common;
using Common.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLib.Extensions
{
    public static class IGraphExtensions
    {
        private static readonly string[] DimensionNames;

        static IGraphExtensions()
        {
            DimensionNames = new string[] { "Width", "Length", "Height" };
        }

        public static string GetInformation(this IGraph self)
        {
            var sb = new StringBuilder();
            var dimensionSizes = self.DimensionsSizes.ToArray();

            for (int i = 0; i < dimensionSizes.Length; i++)
            {
                sb.Append($"{ DimensionNames[i] }: { dimensionSizes[i] }   ");
            }

            sb.Append($"Obstacle percent: { self.GetObstaclesPercent() } ")
              .Append($"({ self.GetObstaclesCount() }/{ self.GetSize() })");

            return sb.ToString();
        }

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

        public static GraphSerializationInfo GetGraphSerializationInfo(this IGraph graph)
        {
            return new GraphSerializationInfo(graph);
        }

        public static void ToUnweighted(this IGraph graph)
        {
            graph.Vertices
                .Where(vertex=>vertex is IWeightableVertex)
                .ForEach(vertex => (vertex as IWeightableVertex).MakeUnweighted());
        }

        public static void ToWeighted(this IGraph graph)
        {
            graph.Vertices
                .Where(vertex => vertex is IWeightableVertex)
                .ForEach(vertex => (vertex as IWeightableVertex).MakeWeighted());
        }

        public static void ConnectVertices(this IGraph self)
        {
            self.Vertices.AsParallel().ForAll(vertex => vertex.SetNeighbours(self));
        }

        public static bool IsEqual(this IGraph self, IGraph graph)
        {
            bool hasEqualSizes = self.GetSize() == graph.GetSize();
            bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == self.GetObstaclesCount();
            bool hasEqualVertices = self.Vertices.Match(graph.Vertices, (a, b) => a.IsEqual(b));
            return hasEqualSizes && hasEqualNumberOfObstacles && hasEqualVertices;
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
