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

        private readonly IReadOnlyDictionary<ICoordinate, IVertex> vertices;

        public IReadOnlyCollection<IVertex> Vertices => (IReadOnlyCollection<IVertex>)vertices.Values;

        public int Size { get; }

        public int[] DimensionsSizes { get; }

        protected BaseGraph(int requiredNumberOfDimensions, IReadOnlyCollection<IVertex> vertices, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes
                .TakeOrDefault(requiredNumberOfDimensions, 1)
                .ToArray();
            Size = DimensionsSizes.GetMultiplication();
            this.vertices = vertices
                .Take(Size)
                .ForAll(new Action<IVertex>(SetGraph))
                .ToDictionary(vertex => vertex.Position)
                .ToReadOnlyDictionary();
        }

        public IVertex Get(ICoordinate coordinate)
        {
            return vertices.GetOrNullVertex(coordinate);
        }

        public override bool Equals(object obj)
        {
            return obj is IGraph graph && graph.IsEqual(this);
        }

        public override int GetHashCode()
        {
            return Vertices
                .Select(x => x.GetHashCode())
                .ToHashCode()
                .Xor(DimensionsSizes.ToHashCode());
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
            vertex
                .GetType()
                .GetFields(NonPublic | Instance)
                .Where(field => field.Name.Contains(nameof(vertex.Graph)))
                .FirstOrDefault()
                .TrySetValue(vertex, this);
        }
    }
}