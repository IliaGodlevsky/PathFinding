using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    internal sealed class Coordinate : ICoordinate
    {
        public int[] CoordinatesValues { get; }

        public Coordinate(int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
            hashCode = new Lazy<int>(() => CoordinatesValues.AggregateOrDefault((x, y) => x ^ y));
        }

        public Coordinate(ICoordinate coordinate)
            : this(coordinate.CoordinatesValues.ToArray())
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

        private readonly Lazy<int> hashCode;
    }
}
