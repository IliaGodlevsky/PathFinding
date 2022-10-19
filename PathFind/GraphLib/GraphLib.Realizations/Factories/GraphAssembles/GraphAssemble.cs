using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;
using System.Collections.Generic;
using System.Linq;
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

        public virtual IGraph AssembleGraph(int obstaclePercent, IReadOnlyList<int> graphDimensionsSizes)
        {
            int graphSize = graphDimensionsSizes.AggregateOrDefault((x, y) => x * y);
            int percentOfObstacles = percentRange.ReturnInRange(obstaclePercent);
            int numberOfObstacles = graphSize.GetPercentage(obstaclePercent);
            var regulars = Enumerable.Repeat(false, graphSize - numberOfObstacles);
            var obstacles = Enumerable.Repeat(true, numberOfObstacles);
            var obstaclesMatrix = regulars.Concat(obstacles).OrderBy(_ => random.Next()).ToReadOnly();
            var vertices = new IVertex[graphSize];
            foreach (var vertexIndex in (0, graphSize))
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

        public override string ToString()
        {
            return "Random cost graph assemble";
        }
    }
}