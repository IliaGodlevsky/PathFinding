using GraphLibrary.Coordinates;
using GraphLibrary.DTO;
using System.Collections.Generic;

namespace GraphLibrary.Vertex.Interface
{
    /// <summary>
    /// Represents an object that can be used to find a path in a graph
    /// </summary>
    public interface IVertex
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        int Cost { get; set; }
        IList<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double AccumulatedCost { get; set; }
        Position Position { get; set; }
        VertexDto Dto { get; }
        void MarkAsEnd();
        void MarkAsSimpleVertex();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
        void MarkAsEnqueued();
    }
}