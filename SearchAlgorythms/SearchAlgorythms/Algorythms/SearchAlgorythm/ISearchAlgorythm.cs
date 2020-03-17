using SearchAlgorythms.Top;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.SearchAlgorythm
{
    public interface ISearchAlgorythm
    {
        bool IsDestination(Button button);
        void Visit(Button button);
        void ExtractNeighbours(Button button);
        void FindDestionation(GraphTop start);
        bool IsRightCellToVisit(Button button);
        void DrawPath(GraphTop end);
        bool DestinationFound { get; set; }
        int GetTime();
    }
}
