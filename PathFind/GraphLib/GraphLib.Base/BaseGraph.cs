using Common.Extensions;
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
    public abstract class BaseGraph : IGraph
    {
        public int Size { get; }

        public int ObstaclePercent => Size == 0 ? 0 : Obstacles * 100 / Size;

        public int Obstacles => Vertices.Count(v => v.IsObstacle);

        public IEnumerable<IVertex> Vertices => vertices.Values;

        public int[] DimensionsSizes { get; }

        protected BaseGraph(int numberOfDimensions, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.ToArray();

            if (dimensionSizes.Length != numberOfDimensions)
            {
                string message = $"An error occurred while creating a {GetType().Name} instance\n";
                message += $"Required number of dimensions is {numberOfDimensions}\n";
                message += "Number of dimensions doesn't match the required number of dimensions";
                int actualValue = DimensionsSizes.Count();

                throw new WrongNumberOfDimensionsException(nameof(dimensionSizes), actualValue, message);
            }

            Size = DimensionsSizes.AggregateOrDefault(IntExtensions.Multiply);
            vertices = new Dictionary<ICoordinate, IVertex>(Size);
        }

        static BaseGraph()
        {
            DimensionNames = new[] { "Width", "Length", "Height" };
        }

        /// <summary>
        /// Get or sets vertex according to a <paramref name="coordinate"/>
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        /// <exception cref="WrongNumberOfDimensionsException"></exception>
        public virtual IVertex this[ICoordinate coordinate]
        {
            get => IsSuitableCoordinate(coordinate)
                   && vertices.TryGetValue(coordinate, out var vertex)
                    ? vertex : new NullVertex();
            set
            {
                if (IsSuitableCoordinate(coordinate))
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
            int verticesHashCode = Vertices
              .Select(x => x.GetHashCode())
              .AggregateOrDefault(IntExtensions.Xor);

            int dimensionSizesHashCode = DimensionsSizes
                .AggregateOrDefault(IntExtensions.Xor);

            return verticesHashCode.Xor(dimensionSizesHashCode);
        }
        public override string ToString()
        {
            string Zip(string name, int size) => $"{name}: {size}";
            var zipped = DimensionNames.Zip(DimensionsSizes, Zip);
            var joined = string.Join("   ", zipped);
            return $"{joined}   Obstacle percent: {ObstaclePercent} ({Obstacles}/{Size})";
        }

        private bool IsSuitableCoordinate(ICoordinate coordinate)
        {
            var coordinates = coordinate.CoordinatesValues;
            if (!coordinates.Any())
            {
                return false;
            }
            if (!coordinates.HaveEqualLength(DimensionsSizes))
            {
                var message = "Dimensions of graph and coordinate doesn't match\n";
                throw new WrongNumberOfDimensionsException(nameof(coordinate), message);
            }
            return true;
        }

        protected static readonly string[] DimensionNames;
        private readonly Dictionary<ICoordinate, IVertex> vertices;
    }
}
