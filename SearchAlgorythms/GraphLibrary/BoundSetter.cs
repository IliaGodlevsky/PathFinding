using SearchAlgorythms.Top;

namespace SearchAlgorythms
{
    public static class BoundSetter
    {
        public static void SetBoundsBetweenNeighbours(IGraphTop top)
        {
            if (top is null)
                return;
            var neighbours = top.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Add(top);
        }

        public static void BreakBoundsBetweenNeighbours(IGraphTop top)
        {
            if (top is null)
                return;
            var neighbours = top.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Remove(top);
            top.Neighbours.Clear();
        }
    }
}
