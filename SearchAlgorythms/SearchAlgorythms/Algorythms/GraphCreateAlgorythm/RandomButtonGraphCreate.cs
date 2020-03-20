using SearchAlgorythms.Top;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SearchAlgorythms.Algorythms.GraphCreateAlgorythm
{
    public class RandomButtonGraphCreate : ICreateAlgorythm
    {
        protected Random rand = new Random();
        private readonly IGraphTop[,] graph;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public RandomButtonGraphCreate(int percentOfObstacles, int width, int height, int buttonWidth, 
            int buttonHeight, int placeBetweenButtons)
        {
            graph = new IGraphTop[width, height];
            for (int xCoordinate = 0; xCoordinate < width; xCoordinate++)
            {
                for (int yCoordinate = 0; yCoordinate < height; yCoordinate++)
                {
                    CreateGraphTop(ref graph[xCoordinate, yCoordinate]);
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        graph[xCoordinate, yCoordinate].IsObstacle = true;
                        graph[xCoordinate, yCoordinate].MarkAsObstacle();
                    }
                    Button button = graph[xCoordinate, yCoordinate] as GraphTop;
                    button.Size = new Size(buttonWidth, buttonHeight);
                    button.Location = new Point((xCoordinate + 1) *
                        placeBetweenButtons + 150, (yCoordinate + 1) * placeBetweenButtons);
                }
            }
            NeigbourSetter setter = new NeigbourSetter(graph);
            setter.SetNeighbours();
        }


        public virtual void CreateGraphTop(ref IGraphTop button)
        {
            button = new GraphTop();
        }

        private bool IsObstacleChance(int percentOfObstacles)
        {
            return rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;
        }

        public IGraphTop[,] GetGraph()
        {
            return graph;
        }
    }
}
