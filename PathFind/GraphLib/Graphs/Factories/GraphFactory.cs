using Common.Extensions;
using GraphLib.Coordinates.Infrastructure.Factories.Interfaces;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static Common.ObjectActivator;

namespace GraphLib.Graphs.Factories
{
    public sealed class GraphFactory<TGraph> : IGraphFactory
        where TGraph : class, IGraph
    {
        public event Action<string> OnExceptionCaught;

        public GraphFactory(IVertexFactory vertexFactory, 
            ICoordinateFactory coordinateFactory)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
        }

        static GraphFactory()
        {
            rand = new Random();
            var ctor = typeof(TGraph).GetConstructor(typeof(int[]));
            RegisterConstructor<TGraph>(ctor);
        }

        public IGraph CreateGraph(int obstaclePercent, params int[] graphDimensionsSizes)
        {
            try
            {
                var activator = GetActivator<TGraph>();
                var graph = activator(graphDimensionsSizes);

                for (int index = 0; index < graph.Size; index++)
                {
                    var coordinates = ToCoordinates(index, graphDimensionsSizes);
                    var coordinate = coordinateFactory.CreateCoordinate(coordinates);

                    graph[coordinate] = vertexFactory.CreateVertex();

                    graph[coordinate].Cost = rand.GetRandomValueCost();
                    if (rand.IsObstacleChance(obstaclePercent))
                    {
                        graph[coordinate].MarkAsObstacle();
                    }

                    graph[coordinate].Position = coordinate;
                }

                graph.ConnectVertices();

                return graph;
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        private IEnumerable<int> ToCoordinates(int index,
                 params int[] dimensions)
        {
            if (index >= dimensions.Aggregate((x, y) => x * y) || index < 0)
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            for (int i = 0; i < dimensions.Length; i++)
            {
                int coordinate = index % dimensions[i];
                index /= dimensions[i];

                yield return coordinate;
            }
        }

        private ICoordinateFactory coordinateFactory;
        private IVertexFactory vertexFactory;

        private static readonly Random rand;
    }
}
