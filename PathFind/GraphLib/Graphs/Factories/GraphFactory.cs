using GraphLib.Coordinates;
using GraphLib.Coordinates.Interface;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Vertex.Interface;
using GraphLib.VertexConnecting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Factories
{
    public class GraphFactory<TGraph> : IGraphFactory
        where TGraph : IGraph
    {
        public GraphFactory(int obstacleChance, params int[] dimensionSizes)
        {
            this.obstacleChance = obstacleChance;
            this.dimensionSizes = dimensionSizes;
        }

        static GraphFactory()
        {
            rand = new Random();
        }

        public IGraph CreateGraph(Func<IVertex> vertexFactoryMethod,
            Func<IEnumerable<int>, ICoordinate> coordinateFactoryMethod)
        {
            try
            {
                var graph = (IGraph)Activator.CreateInstance(typeof(TGraph), dimensionSizes);

                for (int i = 0; i < graph.Size; i++)
                {
                    var coordinates = Index.ToCoordinate(i, dimensionSizes).ToArray();
                    var coordinate = coordinateFactoryMethod?.Invoke(coordinates);

                    graph[coordinate] = vertexFactoryMethod?.Invoke();

                    graph[coordinate].Cost = rand.GetRandomValueCost();
                    if (rand.IsObstacleChance(obstacleChance))
                    {
                        graph[coordinate].MarkAsObstacle();
                    }

                    graph[coordinate].Position = coordinate;
                }

                VertexConnector.ConnectVertices(graph);

                return graph;
            }
            catch
            {
                return new DefaultGraph();
            }
        }

        private readonly int obstacleChance;
        private readonly int[] dimensionSizes;

        private static readonly Random rand;
    }
}
