using Common.Interfaces;
using GraphLib.Coordinates.Abstractions;
using GraphLib.Graphs.Serialization.Infrastructure.Info.Collections;
using GraphLib.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLib.Graphs.Abstractions
{
    /// <summary>
    /// Provides methods for accessing the vertices of the graph, 
    /// as well as for getting information about the graph
    /// </summary>
    public interface IGraph : IEnumerable<IVertex>, IDefault
    {
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