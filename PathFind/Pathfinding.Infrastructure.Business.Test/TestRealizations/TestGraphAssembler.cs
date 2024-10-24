using Pathfinding.Domain.Interface;
using Pathfinding.Domain.Interface.Factories;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestGraphAssemble : IGraphAssemble<TestVertex>
    {
        private readonly GraphAssemble<TestVertex> graphAssemble;

        public TestGraphAssemble()
        {
            var graphFactory = new GraphFactory<TestVertex>();
            var vertexFactory = new TestVertexFactory();
            graphAssemble = new GraphAssemble<TestVertex>(vertexFactory, graphFactory);
        }

        public IGraph<TestVertex> AssembleGraph(IReadOnlyList<int> graphDimensionsSizes)
        {
            var neighborhoodLayer = new NeighborhoodLayer();
            return graphAssemble.AssembleGraph(neighborhoodLayer, graphDimensionsSizes);
        }
    }
}
