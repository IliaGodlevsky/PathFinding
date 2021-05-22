using GraphLib.Interfaces;
using NullObject.Attributes;
using System;
using System.Collections.Generic;

namespace GraphLib.NullRealizations.NullObjects
{
    /// <summary>
    /// An empty coordinate with default realization
    /// </summary>
    [Null]
    [Serializable]
    public sealed class NullCoordinate : ICoordinate
    {
        public IEnumerable<int> CoordinatesValues => new int[] { };

        public override bool Equals(object pos)
        {
            return pos is NullCoordinate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
