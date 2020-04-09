using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorithm
{
    public delegate void PauseCycle(int milliseconds);
    public delegate double HeuristicHandler(IVertex neighbour, IVertex vertex);

    /// <summary>
    /// A base interface of path find algorithms
    /// </summary>
    public interface IPathFindAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool FindDestionation();
        void DrawPath();
        string GetStatistics();
    }
}
