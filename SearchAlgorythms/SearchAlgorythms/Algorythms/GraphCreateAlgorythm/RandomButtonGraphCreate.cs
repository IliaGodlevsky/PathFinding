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
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    CreateGraphTop(ref graph[x, y]);
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        graph[x, y].IsObstacle = true;
                        graph[x, y].MarkAsObstacle();
                    }
                    GraphTop button = graph[x, y] as GraphTop;
                    button.Size = new Size(buttonWidth, buttonHeight);
                    button.Location = new Point((x + 1) *
                        placeBetweenButtons + 150, (y + 1) * placeBetweenButtons);
                }
            }
            NeigbourSetter setter = new NeigbourSetter(graph);
            setter.SetNeighbours();
        }


        public virtual void CreateGraphTop(ref IGraphTop button) => button = new GraphTop();

        private bool IsObstacleChance(int percentOfObstacles) 
            => rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;

        public IGraphTop[,] GetGraph()
        {
            return graph;
        }
    }
}
