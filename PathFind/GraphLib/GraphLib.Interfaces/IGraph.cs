using System.Collections.Generic;

namespace GraphLib.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
     public interface IGraph
    {
        int Size { get; }

        int ObstaclePercent { get; }

        int Obstacles { get; }

        IEnumerable<IVertex> Vertices { get; }

        int[] DimensionsSizes { get; }

        IVertex this[ICoordinate coordinate] { get; set; }
    }
}