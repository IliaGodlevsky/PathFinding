using Common.Attributes;
using GraphLib.Interface;
using System;
using System.Collections.Generic;

namespace GraphLib.Common.NullObjects
{
    /// <summary>
    /// An empty coordinate with default realization
    /// </summary>
    [Default]
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
            return base.GetHashCode();
        }
    }
}
