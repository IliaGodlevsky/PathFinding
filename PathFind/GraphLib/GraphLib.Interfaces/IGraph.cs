using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph : IReadOnlyCollection<IVertex>
    {
        IReadOnlyList<int> DimensionsSizes { get; }

        IVertex Get(ICoordinate coordinate);
    }
}