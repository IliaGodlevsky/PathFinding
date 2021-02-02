using Common.Extensions;
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
    /// <summary>
    /// Assembles a graph suitable for use with pathfinding algorithms
    /// </summary>
    public sealed class GraphAssembler : IGraphAssembler
    {
        public event Action<string> OnExceptionCaught;

        public GraphAssembler(IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory graphFactory)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
        }

        static GraphAssembler()
        {
            rand = new Random();
        }

        /// <summary>
        /// Creates graph with the specification indicated int method params
        /// </summary>
        /// <param name="obstaclePercent"></param>
        /// <param name="graphDimensionsSizes"></param>
        /// <returns>Assembled graph suitable for use with pathfinding algorithms</returns>
        public IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionsSizes)
        {
            try
            {
                var graph = graphFactory.CreateGraph(graphDimensionsSizes);
                graph.ForEachIndex(index => AssembleVertex(graph, index, graphDimensionsSizes, obstaclePercent));
                graph.ConnectVertices();
                return graph;
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        private void AssembleVertex(IGraph graph, int index, 
            int[] graphDimensionsSizes, int obstaclePercent)
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
        private readonly IGraphFactory graphFactory;

        private static readonly Random rand;
    }
}
