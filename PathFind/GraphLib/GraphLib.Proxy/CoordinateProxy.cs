using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace GraphLib.Proxy
{
    [DebuggerDisplay("{ToString()}")]
    internal sealed class CoordinateProxy : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        private IReadOnlyList<int> CoordinatesValues { get; }

        public int Count => CoordinatesValues.Count;

        public int this[int index] => CoordinatesValues[index];

        public CoordinateProxy(params int[] coordinates)
        {
            CoordinatesValues = coordinates.ToReadOnly();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        public bool Equals(ICoordinate other)
        {
            return other.GetHashCode().Equals(GetHashCode());
        }

        public override bool Equals(object pos)
        {
            return pos is ICoordinate coord ? Equals(coord) : false;
        }

        public override int GetHashCode() => hashCode;

        public override string ToString() => toString;

        public IEnumerator<int> GetEnumerator() => CoordinatesValues.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}