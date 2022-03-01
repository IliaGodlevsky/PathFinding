using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Base
{
    [Serializable]
    public abstract class BaseCoordinate : ICoordinate, ICloneable<ICoordinate>
    {
        protected BaseCoordinate(int numberOfDimensions, params int[] coordinates)
        {
            CoordinatesValues = coordinates.TakeOrDefault(numberOfDimensions).ToArray();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
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
            return hashCode;
        }

        public override string ToString()
        {
            return toString;
        }

        public abstract ICoordinate Clone();

        object ICloneable.Clone()
        {
            return Clone();
        }

        private readonly string toString;
        private readonly int hashCode;
    }
}
