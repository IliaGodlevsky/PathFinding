using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Reflection.BindingFlags;

namespace GraphLib.Base
{
    public abstract class BaseGraph : IGraph
    {
        private static readonly string ParamsFormat = "Obstacle percent: {0} ({1}/{2})";
        private const string LargeSpace = "   ";
        protected static readonly string[] DimensionNames = new[] { "Width", "Length", "Height" };

        private readonly Type graphType;
        private readonly object locker = new object();
        private readonly IReadOnlyDictionary<ICoordinate, IVertex> vertices;

        public int Count { get; }

        public int[] DimensionsSizes { get; }

        protected BaseGraph(int requiredNumberOfDimensions, IReadOnlyCollection<IVertex> vertices, params int[] dimensionSizes)
        {
            graphType = GetType();
            DimensionsSizes = dimensionSizes
                .TakeOrDefault(requiredNumberOfDimensions, 1)
                .ToArray();
            Count = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            var verticesMap = vertices
                .Take(Count)
                .ForEach(SetGraph)
                .ToDictionary(vertex => vertex.Position);
            this.vertices = new ReadOnlyDictionary<ICoordinate, IVertex>(verticesMap);
        }

        public IVertex Get(ICoordinate coordinate)
        {
            return vertices.GetOrNullVertex(coordinate);
        }

        public override bool Equals(object obj)
        {
            if (obj is IGraph graph)
            {
                bool hasEqualDimensionSizes = DimensionsSizes.SequenceEqual(graph.DimensionsSizes);
                bool hasEqualNumberOfObstacles = graph.GetObstaclesCount() == this.GetObstaclesCount();
                bool hasEqualVertices = graph.Juxtapose(this, (a, b) => a.Equals(b));
                return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var verticesHashCode = this.Select(x => x.GetHashCode()).ToHashCode();
            var dimensionsHashCode = DimensionsSizes.ToHashCode();
            return HashCode.Combine(verticesHashCode, dimensionsHashCode);
        }

        public override string ToString()
        {
            int obstacles = this.GetObstaclesCount();
            int obstaclesPercent = this.GetObstaclePercent();
            string Zip(string name, int size) => $"{name}: {size}";
            var zipped = DimensionNames.Zip(DimensionsSizes, Zip);
            string joined = string.Join(LargeSpace, zipped);
            string graphParams = string.Format(ParamsFormat,
                obstaclesPercent, obstacles, Count);
            return string.Join(LargeSpace, joined, graphParams);
        }

        private void SetGraph(IVertex vertex)
        {
            lock (locker)
            {
                vertex
                  .GetType()
                  .GetFields(NonPublic | Instance)
                  .Where(field => field.FieldType.IsAssignableFrom(graphType))
                  .ForEach(info => info.SetValue(vertex, this));
            }
        }

        public IEnumerator<IVertex> GetEnumerator() => vertices.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}