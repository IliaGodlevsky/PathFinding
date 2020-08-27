using GraphLibrary.Graph;
using GraphLibrary.Vertex;

namespace GraphLibrary
{
    public class NeigbourSetter
    {
        private readonly AbstractGraph graph;

        public NeigbourSetter(AbstractGraph graph)
        {
            this.graph = graph;
        }

        private bool IsInBounds(int width, int height)
        {
            return width >= 0 && width < graph.Width 
                && height >= 0 && height < graph.Height;
        }

        private bool CanBeNeighbour(IVertex vertex, IVertex neighbour)
        {
            return !neighbour.IsObstacle && vertex != neighbour;
        }

        public void SetNeighbours(IVertex vertex)
        {            
            if (vertex.IsObstacle)
                return;
            var vertexCoordinates = graph.GetIndices(vertex);
            for (int i = vertexCoordinates.X - 1; i <= vertexCoordinates.X + 1; i++)
                for (int j = vertexCoordinates.Y - 1; j <= vertexCoordinates.Y + 1; j++)
                    if (IsInBounds(i, j) && CanBeNeighbour(vertex, graph[i, j]))
                        vertex.Neighbours.Add(graph[i, j]);
        }

        public void SetNeighbours()
        {
            foreach (IVertex vertex in graph)
                SetNeighbours(vertex);
        }
    }
}
