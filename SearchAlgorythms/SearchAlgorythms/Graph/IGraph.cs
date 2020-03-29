using System.Collections;
using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.Graph
{
    public interface IGraph : IEnumerable
    {
        IGraphTop this[int width, int height] { get; set; }

        IGraphTop End { get; set; }
        IGraphTop Start { get; set; }

        IGraphTop[,] GetArray();
        int GetHeight();
        Point GetIndexes(IGraphTop top);
        IGraphTopInfo[,] GetInfo();
        int GetSize();
        int GetWidth();
        void Insert(IGraphTop top);
        int GetObstaclePercent();
        void Refresh();
    }
}