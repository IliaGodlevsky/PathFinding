using System.Collections.Generic;

namespace GraphLib.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraph
    {       
        IEnumerable<IVertex> Vertices { get; }

        IEnumerable<int> DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; set; }

        IVertex this[IEnumerable<int> coordinateValues] { get; set; }

        IVertex this[int index] { get; set; }
    }
}