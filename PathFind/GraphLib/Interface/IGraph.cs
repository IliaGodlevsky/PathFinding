using Common.Interfaces;
using GraphLib.Infrastructure;
using System.Collections.Generic;

namespace GraphLib.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraph : IEnumerable<IVertex>, IDefault
    {
        /// <summary>
        /// Returns a collection of <see cref="VertexSerializationInfo"/>
        /// </summary>
        GraphSerializationInfo SerializationInfo { get; }

        IVertex this[ICoordinate coordinate] { get; set; }

        IEnumerable<int> DimensionsSizes { get; }

        IVertex this[int index] { get; set; }

        int NumberOfVisitedVertices { get; }

        int ObstaclePercent { get; }

        int ObstacleNumber { get; }

        IVertex Start { get; set; }

        IVertex End { get; set; }

        int Size { get; }

        string GetFormattedData(string format);
    }
}