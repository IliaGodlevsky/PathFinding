using System;
using System.Drawing;
using SearchAlgorythms.Top;

namespace SearchAlgorythms.GraphFactory
{
    public class RandomValuedConsoleGraphFactory : IGraphFactory
    {
        protected Random rand = new Random();
        private readonly IGraphTop[,] graph;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public RandomValuedConsoleGraphFactory(int percentOfObstacles, int width, int height)
        {
            graph = new IGraphTop[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    graph[x, y] = new ConsoleGraphTop
                    {
                        Text = (rand.Next(9) + 1).ToString(),
                        Location = new Point(x, y)
                    };
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        graph[x, y].IsObstacle = true;
                        graph[x, y].MarkAsObstacle();
                    }                    
                }
            }
            NeigbourSetter setter = new NeigbourSetter(graph);
            setter.SetNeighbours();
        }

        private bool IsObstacleChance(int percentOfObstacles)
            => rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;

        public IGraphTop[,] GetGraph()
        {
            return graph;
        }
    }
}
