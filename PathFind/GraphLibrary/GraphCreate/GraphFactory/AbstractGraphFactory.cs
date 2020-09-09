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

        public Graph Graph { get; private set; }
        
        public AbstractGraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenVertices)
        {
            Graph = new Graph(width, height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = Graph.GetIndices(vertex);
                vertex = CreateVertex();
                if (rand.IsObstacleChance(percentOfObstacles))                
                    vertex.MarkAsObstacle();
                vertex.SetLocation(new Point(indices.X * placeBetweenVertices, 
                    indices.Y * placeBetweenVertices));
                return vertex;
            }
            Graph.Array.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(Graph);
        }

        static AbstractGraphFactory()
        {
            rand = new Random();
        }

        protected abstract IVertex CreateVertex();

        private static Random rand;
    }
}
