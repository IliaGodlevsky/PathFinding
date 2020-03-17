using SearchAlgorythms.Top;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms
{
    public class NeigbourSetter
    {
        private readonly Button[,] graph;
        private readonly int width;
        private readonly int height;

        public NeigbourSetter(Button[,] graph)
        {
            this.graph = graph;
            width = graph.GetLength(0);
            height = graph.Length / graph.GetLength(0);
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            var top = graph[xCoordinate, yCoordinate] as GraphTop;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height) 
                    {
                        if (graph[i, j] is GraphTop neighbour &&
                            neighbour.BackColor != Color.FromName("Black"))
                            top?.AddNeighbour(neighbour);
                    }
                }
            }
            top?.GetNeighbours().Remove((GraphTop)graph[xCoordinate, yCoordinate]);
        }

        public void SetNeighbours()
        {
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                    SetNeighbours(xCoordinate, yCoordinate);
        }
    }
}
