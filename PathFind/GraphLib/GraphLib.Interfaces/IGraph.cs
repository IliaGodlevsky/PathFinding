using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph
    {
        int Size { get; }

        int[] DimensionsSizes { get; }

        IReadOnlyCollection<IVertex> Vertices { get; }

        IVertex GetByCoordinate(ICoordinate coordinate);

        string ToString();
    }
}