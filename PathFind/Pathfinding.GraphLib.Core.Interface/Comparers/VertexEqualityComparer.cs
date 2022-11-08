using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface.Comparers
{
    public sealed class VertexEqualityComparer : IEqualityComparer<IVertex>
    {
        public bool Equals(IVertex x, IVertex y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(IVertex obj)
        {
            return HashCode.Combine(obj.Cost.CurrentCost, obj.Position);
        }
    }
}
