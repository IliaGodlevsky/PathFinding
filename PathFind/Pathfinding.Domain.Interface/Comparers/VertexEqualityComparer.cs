﻿using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Domain.Interface.Comparers
{
    public sealed class VertexEqualityComparer
        : Singleton<VertexEqualityComparer, IEqualityComparer<IVertex>>, IEqualityComparer<IVertex>
    {
        private VertexEqualityComparer()
        {

        }

        public bool Equals(IVertex x, IVertex y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(IVertex obj)
        {
            return obj.GetHashCode();
        }
    }
}
