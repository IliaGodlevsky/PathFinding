using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph : IReadOnlyCollection<IVertex>
    {
        int[] DimensionsSizes { get; }

        IVertex Get(ICoordinate coordinate);

        string ToString();
    }
}