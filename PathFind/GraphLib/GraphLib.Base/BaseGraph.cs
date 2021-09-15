using Common.Extensions;
using Common.Interface;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

using static Common.Extensions.IntExtensions;

namespace GraphLib.Base
{
    /// <summary>
    /// A base graph for all graph classes.
    /// This is an abstract class
    /// </summary>
    public abstract class BaseGraph : IGraph, ICloneable<IGraph>
    {
        public int Size { get; }

        public IEnumerable<IVertex> Vertices => vertices.Values;

        public int[] DimensionsSizes { get; }

        protected BaseGraph(int numberOfDimensions, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.ToArray();

            if (dimensionSizes.Length != numberOfDimensions)
            {
                throw new WrongNumberOfDimensionsException(nameof(dimensionSizes));
            }

            Size = DimensionsSizes.AggregateOrDefault(IntExtensions.Multiply);
            vertices = new Dictionary<ICoordinate, IVertex>(Size);
        }

        static BaseGraph()
        {
            DimensionNames = new[] { "Width", "Length", "Height" };
        }

        public virtual IVertex this[ICoordinate coordinate]
        {
            get => vertices.TryGetValue(coordinate, out var vertex) ? vertex : new NullVertex();
            set
            {
                if (coordinate.CoordinatesValues.HaveEqualLength(DimensionsSizes))
                {
                    vertices[coordinate] = value;
                }
            }
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
                .AggregateOrDefault(IntExtensions.Xor)
                .Xor(DimensionsSizes.AggregateOrDefault(IntExtensions.Xor));
        }

        public override string ToString()
        {
            string format = "Obstacle percent: {0} ({1}/{2})";
            string largeSpace = "   ";
            int obstacles = this.GetObstaclesCount();
            int obstaclesPercent = this.GetObstaclePercent();
            string Zip(string name, int size) => $"{name}: {size}";
            var zipped = DimensionNames.Zip(DimensionsSizes, Zip);
            string joined = string.Join(largeSpace, zipped);
            string graphParams = string.Format(format,
                obstaclesPercent, obstacles, Size);
            return string.Join(largeSpace, joined, graphParams);
        }

        public abstract IGraph Clone();

        protected static string[] DimensionNames { get; }
        private readonly IDictionary<ICoordinate, IVertex> vertices;
    }
}