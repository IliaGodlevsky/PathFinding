using System.Collections;
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
        GraphTopInfo[,] GetInfo();
        int GetSize();
        int GetWidth();
        void Insert(IGraphTop top);
        int GetObstaclePercent();
        void Refresh();
    }
}