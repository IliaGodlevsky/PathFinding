using SearchAlgorythms.Top;

namespace SearchAlgorythms
{
    public class NeigbourSetter
    {
        private readonly IGraphTop[,] graph;
        private readonly int width;
        private readonly int height;

        public NeigbourSetter(IGraphTop[,] graph)
        {
            this.graph = graph;
            width = graph.GetLength(0);
            height = graph.Length / graph.GetLength(0);
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            if (graph[xCoordinate, yCoordinate].IsObstacle)
                return;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height) 
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
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                    SetNeighbours(xCoordinate, yCoordinate);
        }
    }
}
