using Common.Extensions;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    /// <summary>
    /// Provides base functionality to coordinate classes
    /// </summary>
    [Serializable]
    public abstract class BaseCoordinate : ICoordinate
    {
        protected BaseCoordinate(int numberOfDimensions, params int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            int actualLength = coordinates.Length;
            if (actualLength != numberOfDimensions)
            {
                string argumentName = nameof(coordinates);
                string message = "Number of dimensions must be equal ";
                message += "to coordinates number of dimensions\n";
                message += $"Required value is {numberOfDimensions}";
                throw new WrongNumberOfDimensionsException(argumentName, actualLength, message);
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
            if (!hashCode.HasValue)
            {
                hashCode = CoordinatesValues.AggregateOrDefault(IntExtensions.Xor);
            }
            return hashCode.Value;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(toString))
            {
                var str = string.Join(",", CoordinatesValues);
                toString = $"({str})";
            }
            return toString;
        }

        [NonSerialized]
        private string toString;
        [NonSerialized]
        private int? hashCode;
    }
}
