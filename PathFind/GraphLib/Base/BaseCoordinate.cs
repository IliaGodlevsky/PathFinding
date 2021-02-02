using GraphLib.Extensions;
using GraphLib.Infrastructure;
using GraphLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphLib.Base
{
    /// <summary>
    /// Provides base functionality to coordinate classes
    /// </summary>
    [Serializable]
    public abstract class BaseCoordinate : ICoordinate
    {
        public BaseCoordinate(int numberOfDimensions, params int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            if (CoordinatesValues.Count() != numberOfDimensions)
            {
                throw new ArgumentOutOfRangeException("Number of dimensions must be equal to coordinates number of dimensions");
            }
        }

        public IEnumerable<int> CoordinatesValues { get; }

        public IEnumerable<ICoordinate> Environment
        {
            get
            {
                if (coordinateEnvironment == null)
                {
                    var environment = new CoordinateEnvironment(this);
                    coordinateEnvironment = environment
                        .GetEnvironment()
                        .Select(CreateInstance);
                }

                return coordinateEnvironment;
            }
        }

        public bool IsDefault => false;

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate coordinate)
            {
                return coordinate.IsEqual(this);
            }

            throw new ArgumentException("Invalid value to compare");
        }

        public override int GetHashCode()
        {
            return CoordinatesValues.Aggregate((x, y) => x ^ y);
        }

        public override string ToString()
        {
            var information = new StringBuilder("(");
            var coordinatesInStringRepresentation = CoordinatesValues.
                Select(coordinate => coordinate.ToString());

            for (int i = 0; i < coordinatesInStringRepresentation.Count() - 1; i++)
            {
                information.Append(coordinatesInStringRepresentation.
                    ElementAt(i)).Append(",");
            }

            information.Append(coordinatesInStringRepresentation.Last()).Append(")");
            return information.ToString();
        }

        public abstract object Clone();

        protected abstract ICoordinate CreateInstance(int[] values);

        protected IEnumerable<ICoordinate> coordinateEnvironment;
    }
}
