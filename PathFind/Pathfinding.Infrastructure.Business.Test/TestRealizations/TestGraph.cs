using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Pathfinding.Factories;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Shared.Primitives;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.TestRealizations
{
    internal sealed class TestGraph : Singleton<TestGraph, IGraph<TestVertex>>, IGraph<TestVertex>
    {
        public const int Width = 30;
        public const int Length = 35;

        private readonly IGraph<TestVertex> graph;

        private TestGraph()
        {
            var assemble = new GraphAssemble<TestVertex>(
                new TestVertexFactory(),
                new GraphFactory<TestVertex>());
            var neighborhoodLayer = new NeighborhoodLayer();
            var obstacleLayer = new TestObstacleLayer();
            var costLayer = new TestCostLayer();
            var layers = new Layers.Layers(neighborhoodLayer, obstacleLayer, costLayer);
            graph = assemble.AssembleGraph(layers, Width, Length);
        }

        public IReadOnlyList<int> DimensionsSizes => graph.DimensionsSizes;

        public int Count => graph.Count;

        public TestVertex Get(Coordinate coordinate)
        {
            return graph.Get(coordinate);
        }

        public IEnumerator<TestVertex> GetEnumerator()
        {
            return graph.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
