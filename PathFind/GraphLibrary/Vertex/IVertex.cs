using System.Collections.Generic;
using System.Drawing;

namespace SearchAlgorythms.Top
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
        IVertex ParentTop { get; set; }
        double Value { get; set; }
        Point Location { get; set; }

        VertexInfo GetInfo();
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