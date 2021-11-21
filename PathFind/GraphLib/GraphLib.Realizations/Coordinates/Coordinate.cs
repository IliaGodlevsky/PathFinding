using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    internal sealed class CoordinateProxy : ICoordinate, ICloneable<ICoordinate>
    {
        public int[] CoordinatesValues { get; }

        public CoordinateProxy(int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            hashCode = CoordinatesValues.ToHashCode();
        }

        public CoordinateProxy(ICoordinate coordinate)
            : this(coordinate.CoordinatesValues)
        {

        }

        public override bool Equals(object pos)
        {
            return (pos as ICoordinate)?.IsEqual(this) == true;
        }

        public override int GetHashCode()
        {
            return hashCode;
        }

        public ICoordinate Clone()
        {
            return new CoordinateProxy(this);
        }

        private readonly int hashCode;
    }
}
