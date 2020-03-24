using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public delegate void PauseCycle(int milliseconds);

    public interface ISearchAlgorithm
    {
        PauseCycle Pause { set; get; }
        bool IsDestination(IGraphTop button);
        void Visit(IGraphTop button);
        void ExtractNeighbours(IGraphTop button);
        bool FindDestionation(IGraphTop start);
        bool IsRightCellToVisit(IGraphTop button);
        void DrawPath();
        string GetStatistics();
    }
}
