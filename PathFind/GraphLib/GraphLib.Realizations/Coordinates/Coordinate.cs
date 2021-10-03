using Common.Interface;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    internal sealed class Coordinate : ICoordinate, ICloneable<ICoordinate>
    {
        public int[] CoordinatesValues { get; }

        public Coordinate(int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            hashCode = new Lazy<int>(() => CoordinatesValues.ToHashCode());
        }

        public Coordinate(ICoordinate coordinate)
            : this(coordinate.CoordinatesValues)
        {

        }

        public override bool Equals(object pos)
        {
            return (pos as ICoordinate)?.IsEqual(this) == true;
        }

        public override int GetHashCode()
        {
            return hashCode.Value;
        }

        public ICoordinate Clone()
        {
            return new Coordinate(this);
        }

        [NonSerialized]
        private readonly Lazy<int> hashCode;
    }
}
