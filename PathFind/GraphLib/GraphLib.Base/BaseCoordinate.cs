using GraphLib.Extensions;
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
            int actualLenth = coordinates.Length;
            if (actualLenth != numberOfDimensions)
            {
                var argumentName = nameof(coordinates);
                var message = "Number of dimensions must be equal " +
                    "to coordinates number of dimensions\n";
                message += $"Required value is {numberOfDimensions}";
                throw new ArgumentOutOfRangeException(argumentName, actualLenth, message);
            }
        }

        public IEnumerable<int> CoordinatesValues { get; }

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
            return CoordinatesValues.AggregateOrDefault((x, y) => x ^ y);
        }

        public override string ToString()
        {
            var information = new StringBuilder("(");
            var coordinatesInStringRepresentation = CoordinatesValues.
                Select(coordinate => coordinate.ToString());

            for (int i = 0; i < coordinatesInStringRepresentation.Count() - 1; i++)
            {
                information
                    .Append(coordinatesInStringRepresentation.ElementAt(i))
                    .Append(",");
            }

            information.Append(coordinatesInStringRepresentation.Last()).Append(")");
            return information.ToString();
        }
    }
}
