using GraphLib.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Extensions
{
    internal readonly struct Coordinate : ICoordinate
    {
        public IEnumerable<int> CoordinatesValues { get; }

        public Coordinate(IEnumerable<int> coordinates)
        {
            CoordinatesValues = coordinates.ToArray();
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