using Common.Interface;
using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    public interface IGraph : ICloneable<IGraph>
    {
        /// <summary>
        /// Number of all vertices in the graph
        /// </summary>
        int Size { get; }

        IEnumerable<IVertex> Vertices { get; }

        /// <summary>
        /// Dimension sizes of graph, f.e width, length, height
        /// </summary>
        int[] DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; set; }
    }
}