using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;
using System;
using System.ComponentModel;
using ValueRange;
using ValueRange.Extensions;

using static Common.EnumerableHelper;

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
            this.neighbourhoodFactory = neighbourhoodFactory;
            this.random = random;
            percentRange = new InclusiveValueRange<int>(99, 0);
        }

        /// <summary>
        /// Creates graph with the specification 
        /// indicated int method params
        /// </summary>
        /// <param name="obstaclePercent">A percent of obstacles, 
        /// f.e. 5, 10, 25. Max - 99. If percent is less than 0
        /// it will be set to 0, if greater than 99, will be set
        /// to 99</param>
        /// <param name="graphDimensionsSizes"></param>
        /// <returns>Assembled graph suitable for use with 
        /// pathfinding algorithms</returns>
        /// <exception cref="ArgumentException"></exception>
        public virtual IGraph AssembleGraph(int obstaclePercent, params int[] graphDimensionsSizes)
        {
            int graphSize = graphDimensionsSizes.GetMultiplication();
            int percentOfObstacles = percentRange.ReturnInRange(obstaclePercent);
            var obstaclesMatrix = GetBools(graphSize, obstaclePercent).Shuffle(random.Next);
            var vertices = new IVertex[graphSize];
            for (int vertexIndex = 0; vertexIndex < graphSize; vertexIndex++)
            {
                var coordinateValues = graphDimensionsSizes.ToCoordinates(vertexIndex);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var neighbourhood = neighbourhoodFactory.CreateNeighborhood(coordinate);
                var vertex = vertexFactory.CreateVertex(neighbourhood, coordinate);
                vertex.Cost = costFactory.CreateCost();
                vertex.IsObstacle = obstaclesMatrix[vertexIndex];
                vertices[vertexIndex] = vertex;
            }
            return graphFactory.CreateGraph(vertices, graphDimensionsSizes);
        }

        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly INeighborhoodFactory neighbourhoodFactory;
        protected readonly IRandom random;
        protected readonly InclusiveValueRange<int> percentRange;
    }
}