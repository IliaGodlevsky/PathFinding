using System;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.Core.Interface
{
    public interface IGraph<out TVertex> : IReadOnlyCollection<TVertex>
        where TVertex : IVertex
    {
        IReadOnlyList<int> DimensionsSizes { get; }

        TVertex Get(ICoordinate coordinate);
    }
}