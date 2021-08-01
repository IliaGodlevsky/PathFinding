using Common.Extensions;
using Common.ValueRanges;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Extensions;
using System;
using System.ComponentModel;
using System.Linq;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    /// <summary>
    /// Assembles a graph suitable for use with pathfinding algorithms
    /// </summary>
    [Description("Random graph assemble")]
    public class GraphAssemble : IGraphAssemble
    {
        public GraphAssemble(
            IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            INeighboursCoordinatesFactory neighboursCoordinates)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
            this.neighboursCoordinates = neighboursCoordinates;
            percentRange = new InclusiveValueRange<int>(99, 0);
        }

        /// <summary>
        /// Creates graph with the specification 
        /// indicated int method params
        /// </summary>
        /// <param name="obstaclePercent"></param>
        /// <param name="graphDimensionsSizes"></param>
        /// <returns>Assembled graph suitable for use with 
        /// pathfinding algorithms</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="WrongNumberOfDimensionsException"></exception>
        public IGraph AssembleGraph(int obstaclePercent = 0, params int[] graphDimensionsSizes)
        {
            obstaclePercent = percentRange.ReturnInRange(obstaclePercent);
            var graph = graphFactory.CreateGraph(graphDimensionsSizes);

            void AssembleVertex(int index)
            {
                var coordinateValues = graph.ToCoordinates(index);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var coordinates = neighboursCoordinates.CreateNeighboursCoordinates(coordinate);
                graph[coordinate] = vertexFactory.CreateVertex(coordinates, coordinate);
                graph[coordinate].Cost = costFactory.CreateCost();
                graph[coordinate].IsObstacle = percentRange.IsObstacleChance(obstaclePercent);
            }

            Enumerable.Range(0, graph.Size).ForEach(AssembleVertex);
            return graph.ConnectVertices();
        }

        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly INeighboursCoordinatesFactory neighboursCoordinates;
        protected readonly InclusiveValueRange<int> percentRange;
    }
}
