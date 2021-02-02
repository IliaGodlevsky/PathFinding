using Common;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.NullObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLib.Factories
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
                graph.ForEachIndex(index => AssembleVertex(graph, index, obstaclePercent));
                graph.ConnectVertices();
                return graph;
            }
            catch (Exception ex)
            {
                OnExceptionCaught?.Invoke(ex.Message);
                return new NullGraph();
            }
        }

        private void AssembleVertex(IGraph graph, int index, int obstaclePercent)
        {
            var coordinates = ToCoordinates(index, graph);
            var coordinate = coordinateFactory.CreateCoordinate(coordinates);

            graph[coordinate] = vertexFactory.CreateVertex();

            graph[coordinate].Cost = rand.GetRandomValueCost();
            if (rand.IsObstacleChance(obstaclePercent))
            {
                graph[coordinate].MarkAsObstacle();
            }

            graph[coordinate].Position = coordinate;
        }

        private IEnumerable<int> ToCoordinates(int index, IGraph graph)
        {
            var dimensions = graph.DimensionsSizes.ToArray();

            if (!dimensions.Any())
            {
                throw new ArgumentException("Dimensions count must be grater than 0");
            }

            var rangeOfValidIndexValues = new ValueRange(graph.Size, 0);
            if (!rangeOfValidIndexValues.IsInRange(index))
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            return Enumerable
                .Range(0, dimensions.Length)
                .Select(i => GetCoordinateValue(ref index, dimensions[i]));
        }

        private int GetCoordinateValue(ref int index, int dimensionSize)
        {
            int coordinate = index % dimensionSize;
            index /= dimensionSize;
            return coordinate;
        }

        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IGraphFactory graphFactory;

        private static readonly Random rand;
    }
}
