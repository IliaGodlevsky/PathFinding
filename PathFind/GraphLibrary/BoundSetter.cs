using SearchAlgorythms.Top;

namespace SearchAlgorythms
{
    public static class BoundSetter
    {
        public static void SetBoundsBetweenNeighbours(IVertex vertex)
        {
            if (vertex is null)
                return;
            var neighbours = vertex.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Add(vertex);
        }

        public static void BreakBoundsBetweenNeighbours(IVertex vertex)
        {
            if (vertex is null)
                return;
            var neighbours = vertex.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Remove(vertex);
            vertex.Neighbours.Clear();
        }
    }
}
