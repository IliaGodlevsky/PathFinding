using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public interface IGraph
    {
        Button this[int width, int height] { get; set; }

        GraphTop End { get; set; }
        GraphTop Start { get; set; }

        event EventHandler SetEnd;
        event EventHandler SetStart;
        event MouseEventHandler SwitchRole;

        Button[,] GetArray();
        int GetHeight();
        KeyValuePair<int, int> GetIndexes(Button top);
        GraphTopInfo[,] GetInfo();
        int GetSize();
        int GetWidth();
        void InitializeWith(GraphTopInfo[,] info);
        void Insert(Button top);
        int GetObstaclePercent();
        void Refresh();
        void Reverse(ref Button top);
    }
}