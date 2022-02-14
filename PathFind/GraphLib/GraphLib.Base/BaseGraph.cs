using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphLib.Base
{
    /// <summary>
    /// A base graph for all graph classes.
    /// This is an abstract class
    /// </summary>
    public abstract class BaseGraph : IGraph
    {
        public int Size { get; }

        public IReadOnlyCollection<IVertex> Vertices => vertices.Values;

        public int[] DimensionsSizes { get; }

        public IVertex this[ICoordinate coordinate]
        {
            get => vertices.GetOrNullVertex(coordinate);
        }

        protected BaseGraph(int requiredNumberOfDimensions, IEnumerable<IVertex> vertices, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.TakeOrDefault(requiredNumberOfDimensions).ToArray();
            Size = DimensionsSizes.GetMultiplication();
            this.vertices = vertices.ToDictionary();
            Vertices.ForEach(SetGraphForVertex);
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

        private void SetGraphForVertex(IVertex vertex)
        {
            vertex
                .GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.Name.Contains(nameof(vertex.Graph)))
                .FirstOrDefault()
                .TrySetValue(vertex, this);
        }

        protected static string[] DimensionNames = new[] { "Width", "Length", "Height" };
        private static readonly string ParamsFormat = "Obstacle percent: {0} ({1}/{2})";
        private static readonly string LargeSpace = "   ";

        private readonly Dictionary<ICoordinate, IVertex> vertices;
    }
}