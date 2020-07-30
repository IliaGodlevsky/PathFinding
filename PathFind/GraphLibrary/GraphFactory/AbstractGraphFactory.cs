using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;
using System.Drawing;
using System.Linq;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphFactory : IGraphFactory
    {
        protected Random rand = new Random();
        protected IVertex[,] vertices;

        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenVertices)
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

            IVertex SetLocation(IVertex vertex)
            {
                var indexes = vertices.GetIndices(vertex);
                vertex.Location = new Point(
                    indexes.X * placeBetweenVertices, 
                    indexes.Y * placeBetweenVertices);
                return vertex;
            }


            SetGraph(width, height);
            vertices.Apply(InitializeTop);
            vertices.Apply(SetLocation);
            new NeigbourSetter(vertices).SetNeighbours();
        }

        protected abstract void SetGraph(int width, int height);

        protected abstract IVertex CreateGraphTop();

        public abstract AbstractGraph GetGraph();
    }
}
