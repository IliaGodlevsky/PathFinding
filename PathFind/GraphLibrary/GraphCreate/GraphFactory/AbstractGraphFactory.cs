using System;
using GraphLibrary.Extensions.CollectionExtensions;
using GraphLibrary.Extensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;
using System.Drawing;

namespace GraphLibrary.GraphFactory
{
    public abstract class AbstractGraphFactory 
        : IGraphFactory
    {
        protected IVertex[,] vertices;
        protected int placeBetweenVertices;
        protected Random rand;
        public AbstractGraphFactory(int percentOfObstacles, int width, int height, int placeBetweenVertices)
        {
            this.placeBetweenVertices = placeBetweenVertices;
            vertices = new IVertex[width, height];
            rand = new Random();
            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = vertices.GetIndices(vertex);
                vertex = CreateVertex();
                if (rand.IsObstacleChance(percentOfObstacles))                
                    vertex.MarkAsObstacle();
                vertex.SetLocation(new Point(indices.X * placeBetweenVertices, indices.Y * placeBetweenVertices));
                return vertex;
            }
            vertices.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(GetGraph());
        }

        protected abstract IVertex CreateVertex();

        public Graph GetGraph() => new Graph(vertices);
    }
}
