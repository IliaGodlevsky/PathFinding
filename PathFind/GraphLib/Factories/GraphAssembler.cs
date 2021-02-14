using Common;
using Common.Extensions;
using GraphLib.Extensions;
using GraphLib.Interface;
using System;
using System.Linq;

namespace GraphLib.Factories
{
    /// <summary>
    /// Assembles a graph suitable for use with pathfinding algorithms
    /// </summary>
    public class GraphAssembler : IGraphAssembler
    {
        public event Action<Exception> OnExceptionCaught;

        public GraphAssembler(
            IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
        }

        /// <summary>
        /// Creates graph with the specification indicated int method params
        /// </summary>
        /// <param name="obstaclePercent"></param>
        /// <param name="graphDimensionsSizes"></param>
        /// <returns>Assembled graph suitable for use with pathfinding algorithms</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public IGraph AssembleGraph(int obstaclePercent = 0, params int[] graphDimensionsSizes)
        {
            try
            {
                var graph = graphFactory.CreateGraph(graphDimensionsSizes);
                Enumerable
                    .Range(0, graph.GetSize())
                    .AsParallel()
                    .ForEach(i => AssembleVertex(graph, i, obstaclePercent));
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
            var coordinateValues = graph.ToCoordinates(index);
            var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
            graph[coordinate] = vertexFactory.CreateVertex();
            var vertex = graph[coordinate];
            vertex.Cost = costFactory.CreateCost();
            vertex.IsObstacle = IsObstacleChance(obstaclePercent);
            vertex.Position = coordinate;
        }

        private bool IsObstacleChance(int percentOfObstacles)
        {
            var percentRange = new ValueRange(100, 0);
            percentOfObstacles = percentRange.ReturnInRange(percentOfObstacles);
            var randomPercent = percentRange.GetRandomValueFromRange();
            return randomPercent < percentOfObstacles;
        }

        private readonly IVertexCostFactory costFactory;
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IGraphFactory graphFactory;
    }
}
