using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Base
{
    public abstract class BaseCoordinate : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        private IReadOnlyList<int> CoordinatesValues { get; }

        public int Count => CoordinatesValues.Count;

        public int this[int index] => CoordinatesValues[index];

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

        public override int GetHashCode() => hashCode;

        public override string ToString() => toString;

        public IEnumerator<int> GetEnumerator() => CoordinatesValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => CoordinatesValues.GetEnumerator();
    }
}