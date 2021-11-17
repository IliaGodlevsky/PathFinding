using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Exceptions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;
using Random.Realizations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ValueRange;

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
            INeighborhoodFactory neighbourhoodFactory,
            IRandom random)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
            this.costFactory = costFactory;
            this.neighboursCoordinates = neighbourhoodFactory;
            this.random = random;
            percentRange = new InclusiveValueRange<int>(99, 0);
        }

        public GraphAssemble(
            IVertexFactory vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory graphFactory,
            IVertexCostFactory costFactory,
            INeighborhoodFactory neighbourhoodFactory)
            : this(vertexFactory, coordinateFactory, graphFactory, costFactory,
                  neighbourhoodFactory, new PseudoRandom())
        {

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
        public virtual IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionsSizes)
        {
            var vertices = new List<IVertex>();
            int graphSize = graphDimensionsSizes.AggregateOrDefault(IntExtensions.Multiply);
            int percentOfObstacles = percentRange.ReturnInRange(obstaclePercent);
            for (int vertexIndex = 0; vertexIndex < graphSize; vertexIndex++)
            {
                var coordinateValues = graphDimensionsSizes.ToCoordinates(vertexIndex);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var coordinates = neighboursCoordinates.CreateNeighborhood(coordinate);
                var vertex = vertexFactory.CreateVertex(coordinates, coordinate);
                vertex.Cost = costFactory.CreateCost();
                vertex.IsObstacle = random.Next(percentRange).IsLess(percentOfObstacles);
                vertices.Add(vertex);
            }
            return graphFactory.CreateGraph(vertices, graphDimensionsSizes);
        }

        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly INeighborhoodFactory neighboursCoordinates;
        protected readonly IRandom random;
        protected readonly InclusiveValueRange<int> percentRange;
    }
}
