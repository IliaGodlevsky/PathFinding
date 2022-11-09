using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphAssembles
{
    public class GraphAssemble<TGraph, TVertex> : IGraphAssemble<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        protected readonly IVertexCostFactory costFactory;
        protected readonly ICoordinateFactory coordinateFactory;
        protected readonly IVertexFactory<TVertex> vertexFactory;
        protected readonly IGraphFactory<TGraph, TVertex> graphFactory;
        protected readonly INeighborhoodFactory neighbourhoodFactory;
        protected readonly IRandom random;
        protected readonly InclusiveValueRange<int> percentRange;

        public GraphAssemble(
            IVertexFactory<TVertex> vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory<TGraph, TVertex> graphFactory,
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

        public virtual TGraph AssembleGraph(int obstaclePercent, IReadOnlyList<int> graphDimensionsSizes)
        {
            int graphSize = graphDimensionsSizes.AggregateOrDefault((x, y) => x * y);
            int percentOfObstacles = percentRange.ReturnInRange(obstaclePercent);
            int numberOfObstacles = (int)Math.Round(graphSize * percentOfObstacles / 100.0, 0);
            var regulars = Enumerable.Repeat(false, graphSize - numberOfObstacles);
            var obstacles = Enumerable.Repeat(true, numberOfObstacles);
            var obstaclesMatrix = regulars.Concat(obstacles).Shuffle(random.NextInt).ToReadOnly();
            var vertices = new TVertex[graphSize];
            for (int i = 0; i < graphSize; i++)
            {
                var coordinateValues = ToCoordinates(graphDimensionsSizes, i);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var vertex = vertexFactory.CreateVertex(coordinate);
                vertex.Cost = costFactory.CreateCost();
                vertex.IsObstacle = obstaclesMatrix[i];
                vertices[i] = vertex;
            }
            var graph = graphFactory.CreateGraph(vertices, graphDimensionsSizes);
            graph.ForEach(vertex => SetNeighbourhood(vertex, (IGraph<IVertex>)graph));
            return graph;
        }

        private void SetNeighbourhood(TVertex vertex, IGraph<IVertex> graph)
        {
            var neighbours = neighbourhoodFactory.CreateNeighborhood(vertex.Position);
            vertex.Neighbours = neighbours.GetNeighboursWithinGraph(graph);
        }

        private static IReadOnlyList<int> ToCoordinates(IReadOnlyList<int> dimensionSizes, int index)
        {
            int GetCoordinate(int i)
            {
                var coordinate = index % dimensionSizes[i];
                index /= dimensionSizes[i];
                return coordinate;
            }
            var range = new InclusiveValueRange<int>(dimensionSizes.Count - 1);
            return range.EnumerateValues().Select(GetCoordinate).ToReadOnly();
        }

        public override string ToString()
        {
            return "Random cost graph assemble";
        }
    }
}