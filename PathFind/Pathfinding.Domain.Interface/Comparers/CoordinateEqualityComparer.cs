using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Domain.Interface.Comparers
{
    public sealed class CoordinateEqualityComparer
        : Singleton<CoordinateEqualityComparer, IEqualityComparer<Coordinate>>, IEqualityComparer<Coordinate>
    {
        private CoordinateEqualityComparer()
        {

        }

        public bool Equals(Coordinate x, Coordinate y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Coordinate obj)
        {
            return obj.GetHashCode();
        }
    }
}
