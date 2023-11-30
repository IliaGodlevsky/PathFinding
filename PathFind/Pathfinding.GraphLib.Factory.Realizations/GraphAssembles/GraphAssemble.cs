using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Localizations;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphAssembles
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
            var vertices = new TVertex[graphSize];
            for (int i = 0; i < graphSize; i++)
            {
                var coordinates = ToCoordinates(graphDimensionsSizes, i);
                var coordinate = new Coordinate(coordinates);
                vertices[i] = vertexFactory.CreateVertex(coordinate);
            }
            return graphFactory.CreateGraph(vertices, graphDimensionsSizes);
        }

        private static IReadOnlyList<int> ToCoordinates(IReadOnlyList<int> dimensionSizes, int index)
        {
            var range = new InclusiveValueRange<int>(dimensionSizes.Count - 1);
            int Coordinate(int i)
            {
                var coordinate = index % dimensionSizes[i];
                index /= dimensionSizes[i];
                return coordinate;
            }
            return range.Iterate().Select(Coordinate).ToArray();
        }

        public override string ToString()
        {
            return Languages.RandomGraphAssemble;
        }
    }
}