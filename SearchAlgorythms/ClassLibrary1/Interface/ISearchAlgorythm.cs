using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public interface ISearchAlgorythm
    {
        bool IsDestination(IGraphTop button);
        void Visit(IGraphTop button);
        void ExtractNeighbours(IGraphTop button);
        void FindDestionation(IGraphTop start);
        bool IsRightCellToVisit(IGraphTop button);
        void DrawPath(IGraphTop end);
        bool DestinationFound { get; set; }
        int GetTime();
        bool CanStartSearch();
    }
}
