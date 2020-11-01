using System;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using GraphLib.Coordinates;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interface;
using GraphLib.Extensions;

namespace GraphLib.Graphs.Factories
{
    public class Graph2dFactory : IGraphFactory
    {
        public Graph2dFactory(GraphParametres parametres)
        {
            this.parametres = parametres;
        }

        static Graph2dFactory()
        {
            rand = new Random();
        }


        public IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph2d(parametres.Width, parametres.Height);

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

        private IGraph graph = new DefaultGraph();
        private readonly GraphParametres parametres;
    }
}
