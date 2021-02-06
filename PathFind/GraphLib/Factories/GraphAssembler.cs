using Common;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.VertexCost;
using System;
using System.Linq;

using static System.Linq.Enumerable;

namespace GraphLib.Factories
{
    /// <summary>
    /// Assembles a graph suitable for use with pathfinding algorithms
    /// </summary>
    public sealed class GraphAssembler : IGraphAssembler
    {
        public event Action<Exception> OnExceptionCaught;

        public GraphAssembler(IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory graphFactory)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
        }

        /// <summary>
        /// Creates graph with the specification indicated int method params
        /// </summary>
        /// <param name="obstaclePercent"></param>
        /// <param name="graphDimensionsSizes"></param>
        /// <returns>Assembled graph suitable for use with pathfinding algorithms</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                OnExceptionCaught?.Invoke(ex);
                throw ex;
            }
        }

        private void AssembleVertex(IGraph graph, int index, int obstaclePercent)
        {
            var coordinate = ToCoordinate(index, graph);
            graph[coordinate] = vertexFactory.CreateVertex();
            graph[coordinate].Cost = new Cost();
            if (IsObstacleChance(obstaclePercent))
            {
                graph[coordinate].MarkAsObstacle();
            }
            graph[coordinate].Position = coordinate;
        }

        private bool IsObstacleChance(int percentOfObstacles)
        {
            var percentRange = new ValueRange(100, 0);
            percentOfObstacles = percentRange.ReturnInRange(percentOfObstacles);
            var randomPercent = percentRange.GetRandomValueFromRange();
            return randomPercent < percentOfObstacles;
        }

        private ICoordinate ToCoordinate(int index, IGraph graph)
        {
            var dimensions = graph.DimensionsSizes.ToArray();

            if (!dimensions.Any())
            {
                throw new ArgumentException("Dimensions count must be grater than 0");
            }

            var rangeOfValidIndexValues = new ValueRange(graph.GetSize(), 0);
            if (!rangeOfValidIndexValues.IsInRange(index))
            {
                throw new ArgumentOutOfRangeException("Index is out of range");
            }

            var coordinates = Range(0, dimensions.Length)
                .Select(i => GetCoordinateValue(ref index, dimensions[i]));

            return coordinateFactory.CreateCoordinate(coordinates);
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
    }
}
