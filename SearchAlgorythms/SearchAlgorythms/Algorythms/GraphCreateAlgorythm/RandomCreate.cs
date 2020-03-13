using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class RandomCreate : ICreateAlgorythm
    {
        private Random rand = new Random();
        private Button[,] graph;
        private int width;
        private int height;

        public Button[,] GetGraph(int x, int y)
        {
            width = x;
            height = y;
            graph = new Button[x, y];
            int percentOfObstacles = 25;
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                {                 
                    graph[xCoordinate, yCoordinate] = new GraphTop();
                    if (rand.Next(100/ percentOfObstacles) == 1)
                        graph[xCoordinate, yCoordinate].BackColor = Color.FromName("Black");
                    else
                    {
                        GraphTop temp = graph[xCoordinate, yCoordinate] as GraphTop;
                    }

                }
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++) 
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                    SetNeighbours(xCoordinate, yCoordinate);
            return graph;
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            GraphTop top = graph[xCoordinate, yCoordinate] as GraphTop;
            GraphTop neighbour;
            for (int i = xCoordinate - 1; i <= xCoordinate + 1; i++)
            {
                for (int j = yCoordinate - 1; j <= yCoordinate + 1; j++)
                {
                    if (i >= 0 && i < width && j >= 0 && j < height)
                    {
                        neighbour = graph[i, j] as GraphTop;
                        if (neighbour != null &&
                            neighbour.BackColor != Color.FromName("Black")) 
                            top.AddNeighbour(neighbour);                        
                    }
                }
            }
            top.GetNeighbours().Remove((GraphTop)graph[xCoordinate, yCoordinate]);
        }
    }
}
