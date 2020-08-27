using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System.Linq;

namespace GraphLibrary
{
    public class NeigbourSetter
    {
        private readonly AbstractGraph graph;

        public NeigbourSetter(AbstractGraph graph)
        {
            this.graph = graph;
        }

        public void SetNeighbours(IVertex vertex)
        {
            var coordinates = graph.GetIndices(vertex);
            if (graph[coordinates.X, coordinates.Y].IsObstacle)
                return;
            for (int i = coordinates.X - 1; i <= coordinates.X + 1; i++)
            {
                for (int j = coordinates.Y - 1; j <= coordinates.Y + 1; j++)
                {
                    if (i >= 0 && i < graph.Width && j >= 0 && j < graph.Height) 
                    {
                        if (!graph[i, j].IsObstacle)
                            graph[coordinates.X, coordinates.Y].Neighbours.Add(graph[i, j]);
                    }
                }
            }
            graph[coordinates.X, coordinates.Y].Neighbours.
                Remove(graph[coordinates.X, coordinates.Y]);
        }

        public void SetNeighbours()
        {
            graph.GetArray().
                Cast<IVertex>().ToList().
                ForEach(vertex => SetNeighbours(vertex));
        }
    }
}
