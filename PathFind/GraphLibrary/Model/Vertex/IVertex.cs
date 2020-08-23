using System.Collections.Generic;
using System.Drawing;

namespace GraphLibrary.Vertex
{
    public interface IVertex
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsSimpleVertex { get; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        string Text { get; set; }
        List<IVertex> Neighbours { get; set; }
        IVertex ParentVertex { get; set; }
        double Value { get; set; }
        Point Location { get; set; }

        VertexInfo Info { get; }

        void MarkAsCurrentlyLooked();
        void MarkAsEnd();
        void MarkAsSimpleVertex();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
        void SetToDefault();
    }
}