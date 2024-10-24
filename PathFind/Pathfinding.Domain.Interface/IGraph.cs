﻿using Pathfinding.Shared.Primitives;
using System.Collections.Generic;

namespace Pathfinding.Domain.Interface
{
    public interface IGraph<out TVertex> : IReadOnlyCollection<TVertex>
        where TVertex : IVertex
    {
        IReadOnlyList<int> DimensionsSizes { get; }

        TVertex Get(Coordinate coordinate);
    }
}