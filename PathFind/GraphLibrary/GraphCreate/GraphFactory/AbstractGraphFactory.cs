using GraphLibrary.Extensions.MatrixExtension;
using GraphLibrary.Extensions.RandomExtension;
using GraphLibrary.Graph;
using GraphLibrary.Vertex;
using System;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphFactory 
        : AbstractVertexLocator, IGraphFactory
    {
        protected Random rand;

        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenVertices) : base(placeBetweenVertices)
        {
            rand = new Random();
            IVertex InitializeVertex(IVertex vertex)
            {
                vertex = CreateVertex();
                if (rand.IsObstacleChance(percentOfObstacles))                
                    vertex.MarkAsObstacle();               
                return vertex;
            }
            SetGraph(width, height);
            vertices.Apply(InitializeVertex);
            vertices.Apply(SetLocation);
            VertexLinkManager.ConnectVertices(GetGraph());
        }

        protected abstract IVertex CreateVertex();

        public abstract AbstractGraph GetGraph();
    }
}
