using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Localizations;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.GraphLib.Factory.Realizations.GraphAssembles
{
    public sealed class GraphAssemble<TGraph, TVertex> : IGraphAssemble<TGraph, TVertex>
        where TVertex : IVertex
        where TGraph : IGraph<TVertex>
    {
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory<TVertex> vertexFactory;
        private readonly IGraphFactory<TGraph, TVertex> graphFactory;

        public GraphAssemble(IVertexFactory<TVertex> vertexFactory,
            ICoordinateFactory coordinateFactory,
            IGraphFactory<TGraph, TVertex> graphFactory)
        {
            this.vertexFactory = vertexFactory;
            this.coordinateFactory = coordinateFactory;
            this.graphFactory = graphFactory;
        }

        public TGraph AssembleGraph(IReadOnlyList<int> graphDimensionsSizes)
        {
            int graphSize = graphDimensionsSizes.AggregateOrDefault((x, y) => x * y);
            var vertices = new TVertex[graphSize];
            for (int i = 0; i < graphSize; i++)
            {
                var coordinates = ToCoordinates(graphDimensionsSizes, i);
                var coordinate = coordinateFactory.CreateCoordinate(coordinates);
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
            return range.Enumerate().Select(Coordinate).ToReadOnly();
        }

        public override string ToString()
        {
            return Languages.RandomGraphAssemble;
        }
    }
}