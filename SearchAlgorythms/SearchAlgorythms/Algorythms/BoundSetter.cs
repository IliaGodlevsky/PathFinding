using SearchAlgorythms.Top;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms
{
    public class BoundSetter
    {
        public void SetBoundsBetweenNeighbours(Button top)
        {
            if (top is null)
                return;
            var neighbours = (top as GraphTop).GetNeighbours();
            foreach (var neigbour in neighbours)
                neigbour.AddNeighbour(top as GraphTop);
        }

        public void BreakBoundsBetweenNeighbours(Button top)
        {
            if (top is null)
                return;
            var neighbours = (top as GraphTop).GetNeighbours();
            foreach (var neigbour in neighbours)
                neigbour.GetNeighbours().Remove(top as GraphTop);
        }
    }
}
