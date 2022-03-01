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