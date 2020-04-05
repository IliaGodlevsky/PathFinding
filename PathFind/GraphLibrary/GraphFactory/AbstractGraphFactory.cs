using GraphLibrary.GraphFactory;
using SearchAlgorythms.Top;
using System;
using System.Drawing;

namespace SearchAlgorythms.GraphFactory
{
    public abstract class AbstractGraphFactory : IGraphFactory
    {
        protected Random rand = new Random();
        protected IGraphTop[,] graph;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenButtons)
        {
            SetGraph(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    graph[x, y] = CreateGraphTop();
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        graph[x, y].IsObstacle = true;
                        graph[x, y].MarkAsObstacle();
                    }
                    graph[x, y].Location = new Point(x * placeBetweenButtons, 
                        y * placeBetweenButtons);
                }
            }
            NeigbourSetter setter = new NeigbourSetter(graph);
            setter.SetNeighbours();
        }

        public abstract void SetGraph(int width, int height);
        public abstract IGraphTop CreateGraphTop();

        private bool IsObstacleChance(int percentOfObstacles)
            => rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;

        public IGraphTop[,] GetGraph()
        {
            return graph;
        }
    }
}
