using System.Collections.Generic;
using System.Drawing;
using System;

namespace SearchAlgorythms.Top
{
    public interface IGraphTop
    {
        bool IsEnd { get; set; }
        bool IsObstacle { get; set; }
        bool IsSimpleTop { get; }
        bool IsStart { get; set; }
        bool IsVisited { get; set; }
        string Text { get; set; }
        List<IGraphTop> Neighbours { get; set; }
        IGraphTop ParentTop { get; set; }
        double Value { get; set; }
        Point Location { get; set; }

        IGraphTopInfo GetInfo();
        void MarkAsCurrentlyLooked();
        void MarkAsEnd();
        void MarkAsGraphTop();
        void MarkAsObstacle();
        void MarkAsPath();
        void MarkAsStart();
        void MarkAsVisited();
        void SetToDefault();
    }
}