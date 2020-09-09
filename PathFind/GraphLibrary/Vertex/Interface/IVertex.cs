using GraphLibrary.DTO;
using System.Collections.Generic;
using System.Drawing;

namespace GraphLibrary.Vertex.Interface
{
    public interface IVertex
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        int Cost { get; set; }
        List<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double AccumulatedCost { get; set; }
        Point Location { get; set; }
        VertexDto Info { get; }
        void MarkAsEnd();
        void MarkAsSimpleVertex();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
    }
}