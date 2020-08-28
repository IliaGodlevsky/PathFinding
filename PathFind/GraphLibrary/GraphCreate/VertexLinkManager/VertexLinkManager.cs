using GraphLibrary.Vertex;

namespace GraphLibrary
{
    public static class VertexLinkManager
    {
        public static void ConnectToNeighbours(IVertex vertex)
        {
            if (vertex is null)
                return;
            var neighbours = vertex.Neighbours;
            foreach (var neigbour in neighbours)
                neigbour.Neighbours.Add(vertex);
        }

        public static void IsolateVertex(IVertex vertex)
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
