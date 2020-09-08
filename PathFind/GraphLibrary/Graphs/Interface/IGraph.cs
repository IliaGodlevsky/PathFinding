using GraphLibrary.DTO;
using GraphLibrary.Vertex.Interface;
using System.Collections.Generic;
using System.Drawing;

namespace GraphLibrary.Graphs.Interface
{
    public interface IGraph : IEnumerable<IVertex>
    {
        IVertex this[int width, int height] { get; set; }
        IVertex End { get; set; }
        int Height { get; }
        int NumberOfVisitedVertices { get; }
        int ObstacleNumber { get; }
        int ObstaclePercent { get; }
        int Size { get; }
        IVertex Start { get; set; }
        VertexInfo[,] VerticesInfo { get; }
        int Width { get; }
        IVertex[,] Array { get; }
        Point GetIndices(IVertex vertex);
    }
}