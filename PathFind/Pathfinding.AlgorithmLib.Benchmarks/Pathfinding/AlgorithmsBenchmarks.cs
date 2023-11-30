using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Realizations;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Benchmarks.Pathfinding
{
    public abstract class AlgorithmsBenchmarks
    {
        protected readonly IRandom random = new CongruentialRandom();
        private readonly InclusiveValueRange<int> range = new InclusiveValueRange<int>(5, 1);
        private readonly GraphFactory<TestVertex> graphFactory = new GraphFactory<TestVertex>();
        private readonly INeighborhoodFactory neighbourhood = new MooreNeighborhoodFactory();
        private readonly IVertexFactory<TestVertex> vertexFactory = new TestVertexFactory();
        private readonly IGraphAssemble<TestVertex> graphAssemble;
        private readonly ILayer costLayer;
        private readonly ILayer neighboursLayer;
        private readonly ILayer[] layers;

        protected AlgorithmsBenchmarks()
        {
            graphAssemble = new GraphAssemble<TestVertex>(
                    vertexFactory, graphFactory);
            costLayer = new VertexCostLayer(range, random);
            neighboursLayer = new NeighborhoodLayer(neighbourhood);
            layers = new ILayer[] { costLayer, neighboursLayer };
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

        private IEnumerable<IVertex> GetRange(IGraph<TestVertex> graph)
        {
            yield return graph.First();
            yield return graph.Last();
        }
    }
}
