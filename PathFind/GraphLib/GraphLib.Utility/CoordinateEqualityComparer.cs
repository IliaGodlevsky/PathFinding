using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.Utility
{
    public sealed class CoordinateEqualityComparer : IEqualityComparer<ICoordinate>
    {
        public bool Equals(ICoordinate x, ICoordinate y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(ICoordinate obj)
        {
            return obj.GetHashCode();
        }
    }
}
