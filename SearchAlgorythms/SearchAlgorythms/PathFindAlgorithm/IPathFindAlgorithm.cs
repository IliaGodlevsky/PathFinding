using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    public delegate void PauseCycle(int milliseconds);

    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool FindDestionation(IGraphTop start);
        void DrawPath();
        string GetStatistics();
    }
}
