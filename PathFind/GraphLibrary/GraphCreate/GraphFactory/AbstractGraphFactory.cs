using System;
using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.Extensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphFactory 
        : AbstractVertexLocator, IGraphFactory
    {
        protected Random rand;
        public AbstractGraphFactory(int percentOfObstacles,
            int width, int height, int placeBetweenVertices)
            : base(width, height, placeBetweenVertices)
        {
            rand = new Random();
            IVertex InitializeVertex(IVertex vertex)
            {
                indices = vertices.GetIndices(vertex);
                vertex = CreateVertex();
                if (rand.IsObstacleChance(percentOfObstacles))                
                    vertex.MarkAsObstacle();
                return SetLocation(vertex);
            }
            vertices.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(GetGraph());
        }

        protected abstract IVertex CreateVertex();

        public Graph GetGraph() => new Graph(vertices);
    }
}
