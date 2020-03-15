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
        private int percentOfObstacles = 100;
        private int currentPercent = 54;

        public bool IsObstacleChance()
        {
            return rand.Next(percentOfObstacles) <= currentPercent;
        }

        public Button[,] GetGraph(int x, int y)
        {
            width = x;
            height = y;
            graph = new Button[x, y];
            
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                {
                    if (IsObstacleChance())
                        graph[xCoordinate, yCoordinate] = new Button
                        {BackColor = Color.FromName("Black") };
                    else
                        graph[xCoordinate, yCoordinate] = new GraphTop();
                }
            }
            SetNeighbours();
            return graph;
        }

        public void SetNeighbours(int xCoordinate, int yCoordinate)
        {
            var top = graph[xCoordinate, yCoordinate] as GraphTop;
            // set neighboours in the square around the top of a graph
            // 0  0  0
            // 0  x  0
            // 0  0  0
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
