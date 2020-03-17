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
        private int currentPercent = 44;

        public RandomCreate(int percentOfObstacles)
        {
            currentPercent = percentOfObstacles;
        }

        public bool IsObstacleChance()
        {
            return rand.Next(percentOfObstacles) < currentPercent;
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
                        { BackColor = Color.FromName("Black") };
                    else
                        graph[xCoordinate, yCoordinate] = new GraphTop
                        { BackColor = Color.FromName("White") };
                }
            }
            NeighbourSetter setter = new NeighbourSetter(width, height, graph);
            setter.SetNeighbours();
            return graph;
        }
    }
}
