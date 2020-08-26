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

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            if (graph[xCoordinate, yCoordinate].IsObstacle)
                return;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < graph.Width && j >= 0 && j < graph.Height) 
                    {
                        if (!graph[i, j].IsObstacle)
                            graph[xCoordinate, yCoordinate].Neighbours.Add(graph[i, j]);
                    }
                }
            }
            graph[xCoordinate, yCoordinate].Neighbours.
                Remove(graph[xCoordinate, yCoordinate]);
        }

        public void SetNeighbours()
        {
            graph.GetArray().
                Cast<IVertex>().ToList().
                ForEach(vertex => SetNeighbours(
                    graph.GetIndices(vertex).X, 
                    graph.GetIndices(vertex).Y));
        }
    }
}
