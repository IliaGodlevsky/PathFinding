using Shared.Primitives.Single;
using System.Collections.Generic;

namespace Pathfinding.Domain.Interface.Comparers
{
    public sealed class CoordinateEqualityComparer
        : Singleton<CoordinateEqualityComparer, IEqualityComparer<ICoordinate>>, IEqualityComparer<ICoordinate>
    {
        private CoordinateEqualityComparer()
        {

        }

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
