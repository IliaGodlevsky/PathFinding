using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    public class GraphAssemble : IGraphAssemble
    {
        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory vertexFactory;
        protected readonly IGraphFactory graphFactory;
        protected readonly INeighborhoodFactory neighbourhoodFactory;
        protected readonly IRandom random;
        protected readonly InclusiveValueRange<int> percentRange;

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
            var obstaclesMatrix = (graphSize, obstaclePercent).ToBoolsArray().Shuffle(random.Next);
            var vertices = new IVertex[graphSize];
            for (int vertexIndex = 0; vertexIndex < graphSize; vertexIndex++)
            {
                var coordinateValues = graphDimensionsSizes.ToCoordinates(vertexIndex);
                var vertex = (coordinateFactory, neighbourhoodFactory, vertexFactory).Create(coordinateValues);
                vertex.Cost = costFactory.CreateCost();
                vertex.IsObstacle = obstaclesMatrix[vertexIndex];
                vertices[vertexIndex] = vertex;
            }

            return graphFactory.CreateGraph(vertices, graphDimensionsSizes);
        }

        public override string ToString()
        {
            return "Random cost graph assemble";
        }
    }
}