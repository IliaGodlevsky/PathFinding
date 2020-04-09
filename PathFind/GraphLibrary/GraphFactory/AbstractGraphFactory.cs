using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.GraphFactory;
using SearchAlgorythms.Graph;
using SearchAlgorythms.Top;
using System;
using System.Drawing;

namespace SearchAlgorythms.GraphFactory
{
    public abstract class AbstractGraphFactory : IGraphFactory
    {
        protected Random rand = new Random();
        protected IVertex[,] vertices;
        private const int MAX_PERCENT_OF_OBSTACLES = 100;

        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenVertices)
        {
            SetGraph(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    vertices[x, y] = CreateGraphTop();
                    if (IsObstacleChance(percentOfObstacles))
                    {
                        vertices[x, y].IsObstacle = true;
                        vertices[x, y].MarkAsObstacle();
                    }                    
                }
            }
            vertices.Shuffle();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    vertices[x, y].Location = new Point(x * placeBetweenVertices, y * placeBetweenVertices);
            }
            NeigbourSetter setter = new NeigbourSetter(vertices);
            setter.SetNeighbours();
        }

        protected abstract void SetGraph(int width, int height);
        protected abstract IVertex CreateGraphTop();

        private bool IsObstacleChance(int percentOfObstacles)
            => rand.Next(MAX_PERCENT_OF_OBSTACLES) < percentOfObstacles;

        public abstract AbstractGraph GetGraph();
    }
}
