using GraphLib.Extensions;
using GraphLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Realizations.Coordinates
{
    [Serializable]
    internal sealed class Coordinate : ICoordinate
    {
        public IEnumerable<int> CoordinatesValues { get; }

        public Coordinate(int[] coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
        }

        public Coordinate(ICoordinate coordinate)
        {
            CoordinatesValues = coordinate.CoordinatesValues.ToArray();
        }

        public override bool Equals(object pos)
        {
            return (pos as ICoordinate)?.IsEqual(this) == true;
        }

        public override int GetHashCode()
        {
            return CoordinatesValues.AggregateOrDefault((x, y) => x ^ y);
        }
    }
}
