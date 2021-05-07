using Common.Extensions;
using GraphLib.Common.NullObjects;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLib.Base
{
    public abstract class BaseGraph : IGraph
    {
        public int Size { get; }

        public int ObstaclePercent => Size == 0 ? 0 : Obstacles * 100 / Size;

        public int Obstacles => vertices.Values.Count(v => v.IsObstacle);

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

            Size = DimensionsSizes.AggregateOrDefault((x, y) => x * y);
            vertices = new Dictionary<ICoordinate, IVertex>(Size);
        }

        static BaseGraph()
        {
            DimensionNames = new[] { "Width", "Length", "Height" };
        }

        public IEnumerable<IVertex> Vertices => vertices.Values;

        public IEnumerable<int> DimensionsSizes { get; }

        /// <summary>
        /// Get or sets vertex according to a <paramref name="coordinate"/>
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
                .AggregateOrDefault((x, y) => x ^ y);

            int dimensionSizesHashCode = DimensionsSizes
                .AggregateOrDefault((x, y) => x ^ y);

            return verticesHashCode ^ dimensionSizesHashCode;
        }

        public override string ToString()
        {
            var graphParameters = new StringBuilder();

            void AppendToGraphParameters(int index)
            {
                string dimensionName = DimensionNames[index];
                int dimensionSize = DimensionsSizes.ElementAt(index);
                graphParameters.Append($"{dimensionName}: {dimensionSize}   ");
            }

            int from = 0, to = DimensionsSizes.Count();
            Enumerable.Range(from, to).ForEach(AppendToGraphParameters);

            string obstaclePercentInfo = $"Obstacle percent: {ObstaclePercent} ";
            string obstaclesToSizeRatio = $"({Obstacles}/{Size})";
            string parameters = $"{obstaclePercentInfo}{obstaclesToSizeRatio}";
            graphParameters.Append(parameters);

            return graphParameters.ToString();
        }

        private bool IsSuitableCoordinate(ICoordinate coordinate)
        {
            var coordinates = coordinate.CoordinatesValues.ToArray();
            if (!coordinates.Any())
            {
                return false;
            }
            if (coordinates.Length != DimensionsSizes.Count())
            {
                var message = "Dimensions of graph and coordinate doesn't match\n";
                throw new ArgumentException(message, nameof(coordinate));
            }
            return true;
        }

        protected static readonly string[] DimensionNames;

        private readonly Dictionary<ICoordinate, IVertex> vertices;
    }
}
