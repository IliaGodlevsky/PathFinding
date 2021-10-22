using Common.Extensions;
using Common.Interface;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Base
{
    /// <summary>
    /// Provides base functionality to coordinate classes
    /// </summary>
    [Serializable]
    public abstract class BaseCoordinate : ICoordinate, ICloneable<ICoordinate>
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

        public int[] CoordinatesValues { get; }

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate coordinate)
            {
                return coordinate.IsEqual(this);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (!hashCode.HasValue)
            {
                hashCode = CoordinatesValues.ToHashCode();
            }
            return hashCode.Value;
        }

        public override string ToString()
        {
            return toString = string.IsNullOrEmpty(toString)
                ? $"({string.Join(",", CoordinatesValues)})"
                : toString;
        }

        public abstract ICoordinate Clone();

        [NonSerialized]
        private string toString;
        [NonSerialized]
        private int? hashCode;
    }
}
