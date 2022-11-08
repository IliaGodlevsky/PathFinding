using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface.Comparers
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
