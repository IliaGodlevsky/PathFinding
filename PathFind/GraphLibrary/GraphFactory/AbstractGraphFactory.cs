using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphFactory 
        : AbstractGraphSetter, IGraphFactory
    {
        protected Random rand = new Random();

        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenVertices) : base(placeBetweenVertices)
        {
            IVertex InitializeTop(IVertex vertex)
            {
                vertex = CreateGraphTop();
                if (rand.IsObstacleChance(percentOfObstacles))
                {
                    vertex.IsObstacle = true;
                    vertex.MarkAsObstacle();
                }
                return vertex;
            }

            SetGraph(width, height);
            vertices.Apply(InitializeTop);
            vertices.Apply(SetLocation);
            new NeigbourSetter(vertices).SetNeighbours();
        }

        protected abstract IVertex CreateGraphTop();

        public abstract AbstractGraph GetGraph();
    }
}
