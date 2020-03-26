using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public delegate void PauseCycle(int milliseconds);

    public interface ISearchAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool FindDestionation(IGraphTop start);
        void DrawPath();
        string GetStatistics();
    }
}
