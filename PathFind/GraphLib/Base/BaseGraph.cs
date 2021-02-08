using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            this.RemoveExtremeVertices();
        }

        protected IVertex end;
        public virtual IVertex End
        {
            get => end;
            set { end = value; }
        }

        protected IVertex start;
        public virtual IVertex Start
        {
            get => start;
            set { start = value; }
        }

        public IEnumerable<int> DimensionsSizes { get; private set; }

        public bool IsDefault => false;

        public virtual IVertex this[int index]
        {
            get => vertices[index];
            set => vertices[index] = value;
        }

        public virtual IEnumerator<IVertex> GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vertices.AsEnumerable().GetEnumerator();
        }

        public abstract string GetFormattedData(string format);

        /// <summary>
        /// Get or sets vertex according to a <paramref name="coordinate"/>
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual IVertex this[ICoordinate coordinate]
        {
            get => IsSuitableCoordinate(coordinate)
                ? vertices[coordinate.ToIndex(this)] : new DefaultVertex();
            set
            {
                if (IsSuitableCoordinate(coordinate))
                {
                    vertices[coordinate.ToIndex(this)] = value;
                }
            }
        }

        protected readonly IVertex[] vertices;

        private bool IsSuitableCoordinate(ICoordinate coordinate)
        {
            if (coordinate.IsDefault)
            {
                return false;
            }
            if (coordinate.CoordinatesValues.Count() != DimensionsSizes.Count())
            {
                var message = "Dimensions of graph and coordinate doesn't match\n";
                throw new ArgumentException(message, nameof(coordinate));
            }
            return true;
        }
    }
}
