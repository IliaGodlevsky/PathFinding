using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Layers;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Shared.Extensions;
using Pathfinding.Shared.Primitives;
using Pathfinding.Shared.Random;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Benchmarks.Data
{
    internal sealed class BenchmarkRange :
        Singleton<BenchmarkRange, IEnumerable<BenchmarkVertex>>,
        IEnumerable<BenchmarkVertex>
    {
        private static readonly IGraph<BenchmarkVertex> graph;

        private BenchmarkRange()
        {

        }

        static BenchmarkRange()
        {
            var assemble = new GraphAssemble<BenchmarkVertex>();
            var neighborhoodLayer = new VonNeumannNeighborhoodLayer();
            var random = new CongruentialRandom();
            var costLayer = new VertexCostLayer(
                new InclusiveValueRange<int>(9, 1),
                range => new VertexCost(random.NextInt(range), range));
            var layers = new Layers.Layers(neighborhoodLayer, costLayer);
            graph = assemble.AssembleGraph(layers, 200, 250);
        }

        public IEnumerator<BenchmarkVertex> GetEnumerator()
        {
            yield return graph.Get(10, 10);
            yield return graph.Get(120, 30);
            yield return graph.Get(30, 100);
            yield return graph.Get(10, 110);
            yield return graph.Get(150, 110);
            yield return graph.Get(180, 180);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
