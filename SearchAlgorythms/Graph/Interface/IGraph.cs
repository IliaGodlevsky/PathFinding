using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public interface IGraph
    {
        IGraphTop this[int width, int height] { get; set; }

        IGraphTop End { get; set; }
        IGraphTop Start { get; set; }

        event EventHandler SetEnd;
        event EventHandler SetStart;
        event MouseEventHandler SwitchRole;

        IGraphTop[,] GetArray();
        int GetHeight();
        KeyValuePair<int, int> GetIndexes(IGraphTop top);
        IGraphTopInfo[,] GetInfo();
        int GetSize();
        int GetWidth();
        void InitializeWith(IGraphTopInfo[,] info);
        void Insert(IGraphTop top);
        int GetObstaclePercent();
        void Refresh();
        void Reverse(ref IGraphTop top);
    }
}