using Common.Extensions;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
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
        public int Size => vertices.Count;

        public ICollection<IVertex> Vertices => vertices.Values;

        public int[] DimensionsSizes { get; }

        public IVertex this[ICoordinate coordinate]
        {
            get => vertices.TryGetValue(coordinate, out var vertex) ? vertex : NullVertex.Instance;
        }

        protected BaseGraph(int numberOfDimensions, IEnumerable<IVertex> vertices, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.ToArray();
            int size = DimensionsSizes.AggregateOrDefault(IntExtensions.Multiply);
            this.vertices = vertices.ToDictionary(vertex => vertex.Position);
            if (dimensionSizes.Length != numberOfDimensions || size != Size)
            {
                throw new WrongNumberOfDimensionsException(nameof(dimensionSizes));
            }
            Vertices.ForEach(SetGraphForVertex);
        }

        public override bool Equals(object obj)
        {
            if (obj is IGraph graph)
            {
                return graph.IsEqual(this);
            }

            throw new ArgumentException();
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