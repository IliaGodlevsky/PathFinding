using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;

namespace GraphLib.Proxy
{
    [Serializable]
    [DebuggerDisplay("{ToString()}")]
    internal sealed class CoordinateProxy : ICoordinate
    {
        private readonly string toString;
        private readonly int hashCode;

        public int[] CoordinatesValues { get; }

        public CoordinateProxy(params int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            toString = $"({string.Join(",", CoordinatesValues)})";
            hashCode = CoordinatesValues.ToHashCode();
        }

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate coordinate)
            {
                return this.CoordinatesValues.Juxtapose(coordinate.CoordinatesValues);
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

        public ICoordinate Clone()
        {
            return new CoordinateProxy(CoordinatesValues);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}