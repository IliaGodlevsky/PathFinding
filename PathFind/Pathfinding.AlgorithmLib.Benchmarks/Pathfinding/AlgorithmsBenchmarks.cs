using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random.Realizations;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Benchmarks.Pathfinding
{
    public abstract class AlgorithmsBenchmarks
    {

        private readonly InclusiveValueRange<int> range = new InclusiveValueRange<int>(5, 1);
        private readonly TestGraphFactory graphFactory = new TestGraphFactory();
        private readonly INeighborhoodFactory neighbourhood = new MooreNeighborhoodFactory();
        private readonly ICoordinateFactory coordinateFactory = new TestCoordinateFactory();
        private readonly IVertexFactory<TestVertex> vertexFactory = new TestVertexFactory();
        private readonly IVertexCostFactory costFactory = new TestCostFactory();
        private readonly IGraphAssemble<TestGraph, TestVertex> graphAssemble;
        private readonly ILayer<TestGraph, TestVertex> costLayer;
        private readonly ILayer<TestGraph, TestVertex> neighboursLayer;
        private readonly ILayer<TestGraph, TestVertex>[] layers;

        protected AlgorithmsBenchmarks()
        {
            var random = new DummyRandom();
            graphAssemble = new GraphAssemble<TestGraph, TestVertex>(
                    vertexFactory, coordinateFactory, graphFactory);
            costLayer = new VertexCostLayer<TestGraph, TestVertex>(costFactory, range, random);
            neighboursLayer = new NeighborhoodLayer<TestGraph, TestVertex>(neighbourhood);
            layers = new ILayer<TestGraph, TestVertex>[] { costLayer, neighboursLayer };
        }

        public IEnumerable<IEnumerable<IVertex>> Ranges()
        {
            const int Limit = 15;
            for (int i = 1; i <= Limit; i += 2)
            {
                int dimension = i * 10;
                var graph = graphAssemble.AssembleGraph(layers, i * 10, i * 10);
                yield return GetRange(graph);
            }
        }

        private IEnumerable<IVertex> GetRange(TestGraph graph)
        {
            yield return graph.First();
            yield return graph.Last();
        }
    }
}
