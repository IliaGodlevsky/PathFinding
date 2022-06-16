using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Reflection.BindingFlags;

namespace GraphLib.Base
{
    public abstract class BaseGraph : IGraph
    {       
        private static readonly string ParamsFormat = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string LargeSpace = "   ";
        protected static readonly string[] DimensionNames = new[] { "Width", "Length", "Height" };

        private readonly Type graphType;
        private readonly object locker = new object();
        private readonly IReadOnlyDictionary<ICoordinate, IVertex> vertices;

        public IReadOnlyCollection<IVertex> Vertices => (IReadOnlyCollection<IVertex>)vertices.Values;

        public int Size { get; }

        public int[] DimensionsSizes { get; }

        protected BaseGraph(int requiredNumberOfDimensions, IReadOnlyCollection<IVertex> vertices, params int[] dimensionSizes)
        {
            graphType = GetType();
            DimensionsSizes = dimensionSizes
                .TakeOrDefault(requiredNumberOfDimensions, 1)
                .ToArray();
            Size = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            this.vertices = vertices
                .Take(Size)
                .ForEach(SetGraph)
                .ToDictionary(vertex => vertex.Position)
                .ToReadOnlyDictionary();
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
                bool hasEqualVertices = Vertices.Juxtapose(graph.Vertices, (a, b) => a.Equals(b));
                return hasEqualNumberOfObstacles && hasEqualVertices && hasEqualDimensionSizes;
            }
            return false;
        }

        public override int GetHashCode()
        {
            var verticesHashCode = Vertices.Select(x => x.GetHashCode()).ToHashCode();
            var dimensionsHashCode = DimensionsSizes.ToHashCode();
            return verticesHashCode ^ dimensionsHashCode;
        }

        public override string ToString()
        {
            int obstacles = this.GetObstaclesCount();
            int obstaclesPercent = this.GetObstaclePercent();
            string Zip(string name, int size) => $"{name}: {size}";
            var zipped = DimensionNames.Zip(DimensionsSizes, Zip);
            string joined = string.Join(LargeSpace, zipped);
            string graphParams = string.Format(ParamsFormat,
                obstaclesPercent, obstacles, Size);
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
    }
}