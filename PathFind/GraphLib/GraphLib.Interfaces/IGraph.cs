using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph<out TVertex> : IReadOnlyCollection<TVertex>
        where TVertex : IVertex
    {
        IReadOnlyList<int> DimensionsSizes { get; }

        TVertex Get(ICoordinate coordinate);
    }
}