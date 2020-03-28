using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SearchAlgorithms.Top;

namespace SearchAlgorithms.Graph
{
    public interface IGraph : IEnumerable
    {
        IGraphTop this[int width, int height] { get; set; }

        IGraphTop End { get; set; }
        IGraphTop Start { get; set; }

        IGraphTop[,] GetArray();
        int GetHeight();
        KeyValuePair<int, int> GetIndexes(IGraphTop top);
        IGraphTopInfo[,] GetInfo();
        int GetSize();
        int GetWidth();
        void Insert(IGraphTop top);
        int GetObstaclePercent();
        void Refresh();
    }
}