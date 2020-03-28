using SearchAlgorithms.Top;
using System;
using System.Drawing;

namespace SearchAlgorithms.Algorithms.IGraphFactory
{
    public class RandomButtonGraphFactory : IGraphFactory
    {
        protected Random rand = new Random();
        private readonly IGraphTop[,] graph;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public RandomButtonGraphFactory(int percentOfObstacles, int width, int height, int placeBetweenButtons)
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
                    button.Location = new Point(x * placeBetweenButtons, y * placeBetweenButtons);
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
