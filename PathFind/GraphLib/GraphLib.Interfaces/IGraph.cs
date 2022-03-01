using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph
    {
        int Size { get; }

        IReadOnlyCollection<IVertex> Vertices { get; }

        int[] DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; }
    }
}