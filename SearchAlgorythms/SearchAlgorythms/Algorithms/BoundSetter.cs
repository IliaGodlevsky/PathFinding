using SearchAlgorithms.Top;

namespace SearchAlgorithms.Algorithms
{
    public static class BoundSetter
    {
        public static void SetBoundsBetweenNeighbours(IGraphTop top)
        {
            if (top is null)
                return;
            var neighbours = (top as GraphTop).Neighbours;
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
        }
    }
}
