using Common.Interfaces;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLib.Graphs.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGraph : IEnumerable<IVertex>, IDefault
    {
        /// <summary>
        /// Returns a collection of <see cref="Info.VertexInfo"/>
        /// </summary>
        VertexInfoCollection VertexInfoCollection { get; }

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