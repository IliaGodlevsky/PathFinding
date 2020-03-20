using SearchAlgorythms.Top;

namespace SearchAlgorythms.Algorythms
{
    public class BoundSetter
    {
        public void SetBoundsBetweenNeighbours(IGraphTop top)
        {
            if (top is null)
                return;
            var neighbours = (top as GraphTop).Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Add(top);
        }

        public void BreakBoundsBetweenNeighbours(IGraphTop top)
        {
            if (top is null)
                return;
            var neighbours = top.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Remove(top);
        }
    }
}
