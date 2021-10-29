using Common.Extensions;
using Common.ValueRanges;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    /// <summary>
    /// Assembles a graph suitable for use with pathfinding algorithms
    /// </summary>
    [Description("Random cost graph assemble")]
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
        public virtual IGraph AssembleGraph(int obstaclePercent = 0, params int[] graphDimensionsSizes)
        {
            var vertices = new List<IVertex>();
            int graphSize = graphDimensionsSizes.AggregateOrDefault(IntExtensions.Multiply);
            int percentOfObstacles = percentRange.ReturnInRange(obstaclePercent);
            for (int vertexIndex = 0; vertexIndex < graphSize; vertexIndex++)
            {
                var coordinateValues = graphDimensionsSizes.ToCoordinates(vertexIndex);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var coordinates = neighboursCoordinates.CreateNeighboursCoordinates(coordinate);
                var vertex = vertexFactory.CreateVertex(coordinates, coordinate);
                vertex.Cost = costFactory.CreateCost();
                vertex.IsObstacle = percentRange.IsObstacleChance(percentOfObstacles);
                vertices.Add(vertex);
            }
            return graphFactory.CreateGraph(vertices, graphDimensionsSizes).ConnectVertices();
        }

        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly INeighboursCoordinatesFactory neighboursCoordinates;
        protected readonly InclusiveValueRange<int> percentRange;
    }
}
