using SearchAlgorythms.ButtonExtension;
using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class RandomCreate : ICreateAlgorythm
    {
        protected Random rand = new Random();
        private readonly Button[,] graph;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public RandomCreate(int percentOfObstacles, int width, int height, int buttonWidth, 
            int buttonHeight, int placeBetweenButtons)
        {
            graph = new Button[width, height];
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                {
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        graph[xCoordinate, yCoordinate] = new Button();
                        graph[xCoordinate, yCoordinate].MarkAsObstacle();
                    }
                    else
                        CreateGraphTop(ref graph[xCoordinate, yCoordinate]);
                    graph[xCoordinate, yCoordinate].Size = new Size(buttonWidth, buttonHeight);
                    graph[xCoordinate, yCoordinate].Location = new Point((xCoordinate + 1) *
                        placeBetweenButtons + 150, (yCoordinate + 1) * placeBetweenButtons);
                }
            }
            NeigbourSetter setter = new NeigbourSetter(graph);
            setter.SetNeighbours();
        }


        public virtual void CreateGraphTop(ref Button button)
        {
            button = new GraphTop();
        }

        private bool IsObstacleChance(int percentOfObstacles)
        {
            return rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;
        }

        public Button[,] GetGraph()
        {
            return graph;
        }
    }
}
