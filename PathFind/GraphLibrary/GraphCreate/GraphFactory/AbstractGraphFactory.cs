using System;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;
using System.Drawing;
using GraphLibrary.Extensions.CustomTypeExtensions;

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
                vertex.Cost = rand.GetRandomValueCost();
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

        private static readonly Random rand;
    }
}
