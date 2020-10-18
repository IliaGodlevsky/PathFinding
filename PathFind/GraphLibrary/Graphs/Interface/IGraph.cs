using GraphLibrary.Coordinates.Interface;
using GraphLibrary.DTO.Interface;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;

namespace GraphLibrary.Graphs.Interface
{
    public struct GraphParametres
    {
        public int Width { get; }
        public int Height { get; }
        public int ObstaclePercent { get; }

        public GraphParametres(int width,
            int height, int obstaclePercent)
        {
            Width = width;
            Height = height;
            ObstaclePercent = obstaclePercent;
        }
    }

    /// <summary>
    /// Provides methods for accessing the vertices of the graph, 
    /// as well as for getting information about the graph
    /// </summary>
    public interface IGraph : IEnumerable<IVertex>
    {
        int Size { get; }
        int NumberOfVisitedVertices { get; }
        int ObstacleNumber { get; }
        int ObstaclePercent { get; }
        IVertex this[ICoordinate coordinate] { get; set; }
        IVertex End { get; set; }
        IVertex Start { get; set; }
        IVertexDtoContainer VertexDtos { get; }
        string GetFormattedData(string format);
        IEnumerable<int> DimensionsSizes { get; }
    }
}