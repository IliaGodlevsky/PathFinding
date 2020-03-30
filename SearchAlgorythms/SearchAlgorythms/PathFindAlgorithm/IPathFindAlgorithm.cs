using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    public delegate void PauseCycle(int milliseconds);

    public interface IPathFindAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool FindDestionation(IGraphTop start);
        void DrawPath();
        string GetStatistics();
    }
}
