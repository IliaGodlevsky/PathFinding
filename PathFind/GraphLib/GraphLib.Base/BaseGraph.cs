using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLib.Base
{
    public abstract class BaseGraph : IGraph
    {
        public BaseGraph(int numberOfDimensions, params int[] dimensionSizes)
        {
            DimensionsSizes = dimensionSizes.ToArray();

            if (dimensionSizes.Length != numberOfDimensions)
            {
                var message = $"An error ocurred while creating a {GetType().Name} instance\n";
                message += $"Required number of dimensions is {numberOfDimensions}\n";
                message += "Number of dimensions doesn't match the required number of dimensions\n";
                var actualValue = dimensionSizes.Count();

                throw new ArgumentOutOfRangeException(nameof(dimensionSizes), actualValue, message);
            }

            vertices = new IVertex[this.GetSize()];
        }

        static BaseGraph()
        {
            DimensionNames = new string[] { "Width", "Length", "Height" };
        }

        public IEnumerable<IVertex> Vertices => vertices;

        public IEnumerable<int> DimensionsSizes { get; private set; }

        public virtual IVertex this[IEnumerable<int> coordinateValues]
        {
            get
            {
                return IsSuitableCoordinate(coordinateValues)
               ? vertices[coordinateValues.ToIndex(DimensionsSizes.ToArray())] 
               : new DefaultVertex();
            }
            set
            {
                if (IsSuitableCoordinate(coordinateValues))
                {
                    vertices[coordinateValues.ToIndex(DimensionsSizes.ToArray())] = value;
                }
            }
        }

        public virtual IVertex this[int index]
        {
            get => vertices[index];
            set => vertices[index] = value;
        }

        /// <summary>
        /// Get or sets vertex according to a <paramref name="coordinate"/>
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual IVertex this[ICoordinate coordinate]
        {
            get => this[coordinate.CoordinatesValues];
            set => this[coordinate.CoordinatesValues] = value;
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
            var verticesHashCode = Vertices
                .Select(x => x.GetHashCode())
                .AggregateOrDefault((x, y) => x ^ y);

            var dimensionSizesHashCode = DimensionsSizes
                .AggregateOrDefault((x, y) => x ^ y);

            return verticesHashCode ^ dimensionSizesHashCode;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            var dimensionSizes = DimensionsSizes.ToArray();

            for (int i = 0; i < dimensionSizes.Length; i++)
            {
                sb.Append($"{ DimensionNames[i] }: { dimensionSizes[i] }   ");
            }

            sb.Append($"Obstacle percent: { this.GetObstaclesPercent() } ")
              .Append($"({ this.GetObstaclesCount() }/{ this.GetSize() })");

            return sb.ToString();
        }

        private bool IsSuitableCoordinate(IEnumerable<int> coordinateValues)
        {
            if (!coordinateValues.Any())
            {
                return false;
            }
            if (coordinateValues.Count() != DimensionsSizes.Count())
            {
                var message = "Dimensions of graph and coordinate doesn't match\n";
                throw new ArgumentException(message, nameof(coordinateValues));
            }
            return true;
        }

        protected static readonly string[] DimensionNames;

        private readonly IVertex[] vertices;
    }
}
