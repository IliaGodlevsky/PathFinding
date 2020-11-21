using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Coordinates
{
    [Serializable]
    public sealed class DefaultCoordinate : ICoordinate
    {
        public IEnumerable<int> Coordinates => new int[] { };

        public IEnumerable<ICoordinate> Environment => new DefaultCoordinate[] { };

        public bool IsDefault => true;

        public DefaultCoordinate()
        {

        }

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate coordinate)
            {
                return coordinate.IsEqual(this);
            }

            throw new ArgumentException("Invalid value to compare");
        }

        public override int GetHashCode()
        {
            return Coordinates.Aggregate((x, y) => x ^ y);
        }

        public object Clone()
        {
            return new DefaultCoordinate();
        }
    }
}
