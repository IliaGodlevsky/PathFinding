using System.Drawing;

namespace SearchAlgorithms
{
    public interface IGraphTopInfo
    {
        string Colour { get; set; }
        bool IsObstacle { get; set; }
        string Text { get; set; }
        Point Location { get; set; }
    }
}