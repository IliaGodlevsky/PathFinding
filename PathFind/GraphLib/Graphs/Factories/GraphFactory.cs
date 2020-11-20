using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
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
    public sealed class GraphFactory<TGraph> : IGraphFactory
        where TGraph : IGraph
    {
        public event Action<string> OnExceptionCaught;

        public GraphFactory(int obstacleChance, params int[] dimensionSizes)
        {
            this.obstacleChance = obstacleChance;
            this.dimensionSizes = dimensionSizes;
        }

        static GraphFactory()
        {
            rand = new Random();
        }

        public IGraph CreateGraph(Func<IVertex> vertexCreateMethod,
            Func<IEnumerable<int>, ICoordinate> coordinateCreateMethod)
        {
            try
            {
                var graph = (IGraph)Activator.CreateInstance(typeof(TGraph), dimensionSizes);

                for (int i = 0; i < graph.Size; i++)
                {
                    var coordinates = Index.ToCoordinate(i, dimensionSizes).ToArray();
                    var coordinate = coordinateCreateMethod?.Invoke(coordinates);

                    graph[coordinate] = vertexCreateMethod?.Invoke();

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
            catch(Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new DefaultGraph();
            }
        }

        private readonly int obstacleChance;
        private readonly int[] dimensionSizes;

        private static readonly Random rand;
    }
}
