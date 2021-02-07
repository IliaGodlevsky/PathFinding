using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.NullObjects
{
    /// <summary>
    /// An empty coordinate with default realization
    /// </summary>
    [Serializable]
    public sealed class DefaultCoordinate : ICoordinate
    {
        public IEnumerable<int> CoordinatesValues => new int[] { };

        public IEnumerable<ICoordinate> Environment => new DefaultCoordinate[] { };

        public bool IsDefault => true;

        public DefaultCoordinate()
        {

        }

        public override bool Equals(object pos)
        {
            if (pos is ICoordinate)
            {
                return false;
            }

            throw new ArgumentException("Invalid value to compare");
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public object Clone()
        {
            return new DefaultCoordinate();
        }
    }
}
