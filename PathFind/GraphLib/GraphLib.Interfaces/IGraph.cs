using System.Collections.Generic;
using ValueRange;

namespace GraphLib.Interfaces
{
    public interface IGraph<out TVertex> : IReadOnlyCollection<TVertex>
        where TVertex : IVertex
    {
        InclusiveValueRange<int> CostRange { get; set; }

        IReadOnlyList<int> DimensionsSizes { get; }

        TVertex Get(ICoordinate coordinate);
    }
}