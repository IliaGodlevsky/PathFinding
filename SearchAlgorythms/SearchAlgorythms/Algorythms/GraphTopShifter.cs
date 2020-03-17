using SearchAlgorythms.Top;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms
{
    public class GraphTopShifter
    {
        public void InsertGraphTop(Button top, Button[,] buttons, int width, int height)
        {
            var coordiantes = NeighbourSetter.GetCoordinates(top, buttons, width, height);
            buttons[coordiantes.Key, coordiantes.Value] = top;
            NeighbourSetter setter = new NeighbourSetter(width, height, buttons);
            setter.SetNeighbours(coordiantes.Key, coordiantes.Value);
        }

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
