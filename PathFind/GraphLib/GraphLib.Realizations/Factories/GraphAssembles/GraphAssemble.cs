using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Realizations.Factories.GraphAssembles
{
    public class GraphAssemble<TGraph, TVertex> : IGraphAssemble<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        protected readonly InclusiveValueRange<int> costRange;
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
            costRange = new InclusiveValueRange<int>(9, 1);
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
                var coordinateValues = graphDimensionsSizes.ToCoordinates(i);
                var coordinate = coordinateFactory.CreateCoordinate(coordinateValues);
                var vertex = vertexFactory.CreateVertex(coordinate);
                int cost = random.NextInt(costRange);
                vertex.Cost = costFactory.CreateCost(cost);
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

        public override string ToString()
        {
            return "Random cost graph assemble";
        }
    }
}