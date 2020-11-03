using GraphLib.Coordinates;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interface;
using GraphLib.Vertex.Cost;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;

namespace GraphLib.Graphs.Factories
{
    public class Graph3dFactory : IGraphFactory
    {
        public Graph3dFactory(int width, int length, int height, int obstacleChance)
        {
            this.width = width;
            this.length = length;
            this.height = height;
            this.obstacleChance = obstacleChance;
        }

        static Graph3dFactory()
        {
            rand = new Random();
        }


        public IGraph CreateGraph(Func<IVertex> vertexFactory)
        {
            graph = new Graph3d(width, length, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    for (int l = 0; l < height; l++)
                    {
                        var indices = new Coordinate3D(i, j, l);
                        graph[indices] = vertexFactory();
                        graph[indices].Cost = rand.GetRandomValueCost();
                        if (rand.IsObstacleChance(obstacleChance))
                            graph[indices].MarkAsObstacle();
                        graph[indices].Position = indices;
                    }
                }
            }

            VertexConnector.ConnectVertices(graph);

            return graph;
        }

        private static readonly Random rand;

        private IGraph graph = new DefaultGraph();
        private readonly int width;
        private readonly int length;
        private readonly int height;
        private readonly int obstacleChance;
    }
}
