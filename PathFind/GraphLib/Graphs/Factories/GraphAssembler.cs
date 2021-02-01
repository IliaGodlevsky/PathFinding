using GraphLib.Coordinates.Infrastructure.Factories.Interfaces;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories.Interfaces;
using GraphLib.Vertex.Infrastructure.Factories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Graphs.Factories
{
    public sealed class GraphAssembler : IGraphAssembler
    {
        public event Action<string> OnExceptionCaught;

        public GraphAssembler(IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory initializer)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            graphInitializer = initializer;
        }

        static GraphAssembler()
        {
            rand = new Random();
        }

        public IGraph CreateGraph(int obstaclePercent, params int[] graphDimensionsSizes)
        {
            try
            {
                var graph = graphInitializer.CreateGraph(graphDimensionsSizes);

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

        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IGraphFactory graphInitializer;

        private static readonly Random rand;
    }
}
