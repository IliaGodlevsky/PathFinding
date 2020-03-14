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
        bool IsRightCell(Button button);
        void DrawPath(GraphTop end);
    }
}
