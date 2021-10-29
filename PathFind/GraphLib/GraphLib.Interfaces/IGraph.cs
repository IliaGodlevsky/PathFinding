using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph
    {
        /// <summary>
        /// Number of all vertices in the graph
        /// </summary>
        int Size { get; }

        ICollection<IVertex> Vertices { get; }

        /// <summary>
        /// Dimension sizes of graph, f.e width, length, height
        /// </summary>
        int[] DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; }
    }
}