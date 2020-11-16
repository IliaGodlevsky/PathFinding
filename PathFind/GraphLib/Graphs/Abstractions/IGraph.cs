using Common.Interfaces;
using GraphLib.Coordinates.Interface;
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
        int Size { get; }

        int NumberOfVisitedVertices { get; }

        int ObstacleNumber { get; }

        int ObstaclePercent { get; }

        IVertex this[ICoordinate coordinate] { get; set; }

        IVertex this[int index] { get; set; }

        IVertex End { get; set; }

        IVertex Start { get; set; }

        VertexInfoCollection VertexInfoCollection { get; }

        IEnumerable<int> DimensionsSizes { get; }

        string GetFormattedData(string format);
    }
}