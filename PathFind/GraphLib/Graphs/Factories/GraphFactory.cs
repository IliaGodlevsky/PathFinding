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
            graph = new DefaultGraph();
            this.dimensionSizes = dimensionSizes;
        }

        static GraphFactory()
        {
            rand = new Random();
        }

        public IGraph CreateGraph(Func<IVertex> vertexFactory,
            Func<IEnumerable<int>, ICoordinate> coordinateFactory)
        {
            try
            {
                graph = (IGraph)Activator.CreateInstance(typeof(TGraph), dimensionSizes);

                for (int i = 0; i < graph.Size; i++)
                {
                    var coordinates = Index.ToCoordinate(i, dimensionSizes).ToArray();
                    var coordinate = coordinateFactory(coordinates);
                    CreateVertex(vertexFactory, coordinate);
                }

                VertexConnector.ConnectVertices(graph);
            }
            catch
            {
                return new DefaultGraph();
            }

            return graph;
        }

        private void CreateVertex(Func<IVertex> vertexFactory,
            ICoordinate coordinate)
        {
            graph[coordinate] = vertexFactory();
            graph[coordinate].Cost = rand.GetRandomValueCost();

            if (rand.IsObstacleChance(obstacleChance))
            {
                graph[coordinate].MarkAsObstacle();
            }

            graph[coordinate].Position = coordinate;
        }

        private IGraph graph;
        private readonly int obstacleChance;
        private readonly int[] dimensionSizes;

        private static readonly Random rand;
    }
}
