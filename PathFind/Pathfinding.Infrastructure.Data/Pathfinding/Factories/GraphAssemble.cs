using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Infrastructure.Data.Pathfinding.Factories
{
    public sealed class GraphAssemble<TVertex> : IGraphAssemble<TVertex>
        where TVertex : IVertex
    {
        private readonly IVertexFactory<TVertex> vertexFactory;
        private readonly IGraphFactory<TVertex> graphFactory;

        public GraphAssemble(IVertexFactory<TVertex> vertexFactory,
            IGraphFactory<TVertex> graphFactory)
        {
            this.vertexFactory = vertexFactory;
            this.graphFactory = graphFactory;
        }

        public IGraph<TVertex> AssembleGraph(IReadOnlyList<int> graphDimensionsSizes)
        {
            int graphSize = graphDimensionsSizes.AggregateOrDefault((x, y) => x * y);
            var vertices = Enumerable.Range(0, graphSize)
                .Select(i => vertexFactory.CreateVertex(ToCoordinates(graphDimensionsSizes, i)))
                .ToArray();
            return graphFactory.CreateGraph(vertices, graphDimensionsSizes);
        }

        private static ICoordinate ToCoordinates(IReadOnlyList<int> dimensionSizes, int index)
        {
            var range = new InclusiveValueRange<int>(dimensionSizes.Count - 1);
            int Coordinate(int i)
            {
                var coordinate = index % dimensionSizes[i];
                index /= dimensionSizes[i];
                return coordinate;
            }
            var coordinates = range.Iterate().Select(Coordinate).ToArray();
            return new Coordinate(coordinates);
        }
    }
}