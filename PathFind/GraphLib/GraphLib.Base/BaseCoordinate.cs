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
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        public int[] CoordinatesValues { get; }

        public override bool Equals(object pos) => (pos as ICoordinate)?.IsEqual(this) == true;
        public override int GetHashCode() => hashCode;
        public override string ToString() => toString;

        public abstract ICoordinate Clone();

        [NonSerialized]
        private readonly string toString;
        [NonSerialized]
        private readonly int hashCode;
    }
}
