using System;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.VertexBinding;
using System.Drawing;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreate.GraphFactory.Interface;

namespace GraphLibrary.GraphFactory
{
    public class GraphFactory : IGraphFactory
    {
        public GraphFactory(int percentOfObstacles, 
            int width, int height, int placeBetweenVertices)
        {
            this.percentOfObstacles = percentOfObstacles;
            this.width = width;
            this.height = height;
            this.placeBetweenVertices = placeBetweenVertices;
        }

        static GraphFactory()
        {
            rand = new Random();
        }


        public Graph GetGraph(Func<IVertex> generator)
        {
            graph = new Graph(width, height);

            IVertex InitializeVertex(IVertex vertex)
            {
                var indices = graph.GetIndices(vertex);
                vertex = generator();
                vertex.Cost = rand.GetRandomValueCost();
                if (rand.IsObstacleChance(percentOfObstacles))
                    vertex.MarkAsObstacle();
                vertex.SetLocation(new Point(indices.X * placeBetweenVertices,
                    indices.Y * placeBetweenVertices));
                return vertex;
            }

            graph.Array.Apply(InitializeVertex);
            VertexBinder.ConnectVertices(graph);

            return graph;
        }

        private static readonly Random rand;

        private Graph graph;
        private readonly int width;
        private readonly int height;
        private readonly int placeBetweenVertices;
        private readonly int percentOfObstacles;
    }
}
