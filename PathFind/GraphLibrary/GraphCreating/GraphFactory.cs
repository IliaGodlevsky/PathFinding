using System;
using GraphLibrary.Extensions.SystemTypeExtensions;
using GraphLibrary.Graphs;
using GraphLibrary.Vertex.Interface;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.GraphCreating.Interface;
using GraphLibrary.VertexConnecting;
using GraphLibrary.Coordinates;

namespace GraphLibrary.GraphCreating
{
    public class GraphFactory : IGraphFactory
    {
        public GraphFactory(GraphParametres parametres)
        {
            this.parametres = parametres;
        }

        static GraphFactory()
        {
            rand = new Random();
        }


        public IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph(parametres.Width, parametres.Height);
            for (int i = 0; i < parametres.Width; i++)
            {
                for (int j = 0; j < parametres.Height; j++)
                {
                    var indices = new Coordinate2D(i, j);
                    graph[indices] = vertexFactory();
                    graph[indices].Cost = rand.GetRandomValueCost();
                    if (rand.IsObstacleChance(parametres.ObstaclePercent))
                        graph[indices].MarkAsObstacle();
                    graph[indices].Position = indices;
                }
            }
            VertexConnector.ConnectVertices(graph);
            return graph;
        }

        private static readonly Random rand;

        private IGraph graph = NullGraph.Instance;
        private readonly GraphParametres parametres;
    }
}
