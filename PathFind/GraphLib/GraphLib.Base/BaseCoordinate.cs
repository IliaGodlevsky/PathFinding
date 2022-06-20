using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseCoordinate : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        public int[] CoordinatesValues { get; }

        protected BaseCoordinate(int numberOfDimensions, params int[] coordinates)
        {
            CoordinatesValues = coordinates.TakeOrDefault(numberOfDimensions).ToArray();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        public bool Equals(ICoordinate other)
        {
            return other.GetHashCode().Equals(GetHashCode());
        }

        public override bool Equals(object pos)
        {
            return pos.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public override string ToString()
        {
            return toString;
        }
    }
}