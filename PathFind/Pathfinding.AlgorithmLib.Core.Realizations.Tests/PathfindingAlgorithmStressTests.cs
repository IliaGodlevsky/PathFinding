using NUnit.Framework;
using Pathfinding.AlgorithmLib.Core.Interface;
using Pathfinding.AlgorithmLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Realizations;
using System.Linq;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests
{
    using AlgorithmFactory = IAlgorithmFactory<IAlgorithm<IGraphPath>>;

    [TestFixture]
    [Explicit]
    public class PathfindingAlgorithmStressTests
    {
        private readonly string wrongPathMsg = "Path was not found";

        private readonly IRandom random = new CongruentialRandom();
        private readonly InclusiveValueRange<int> range = new InclusiveValueRange<int>(5, 1);
        private readonly GraphFactory<TestVertex> graphFactory = new GraphFactory<TestVertex>();
        private readonly INeighborhoodFactory neighbourhood = new MooreNeighborhoodFactory();
        private readonly ICoordinateFactory coordinateFactory = new CoordinateFactory();
        private readonly IVertexFactory<TestVertex> vertexFactory = new TestVertexFactory();
        private readonly IVertexCostFactory costFactory = new TestCostFactory();
        private readonly IGraphAssemble<TestVertex> graphAssemble;
        private readonly ILayer[] layers;

        public PathfindingAlgorithmStressTests()
        {
            graphAssemble = new GraphAssemble<TestVertex>(
                vertexFactory, coordinateFactory, graphFactory);
            var costLayer = new VertexCostLayer(costFactory, range, random);
            var neighboursLayer = new NeighborhoodLayer(neighbourhood);
            layers = new ILayer[] { costLayer, neighboursLayer };
        }

        //[TestCaseSource(typeof(FindPathTestCaseData), nameof(FindPathTestCaseData.Factories))]
        //[Explicit]
        public void FindPath_Stress(AlgorithmFactory factory)
        {
            const int Limit = 1500;
            for (int i = 1; i <= Limit; i += 10)
            {
                var graph = graphAssemble.AssembleGraph(layers, i * 10, i * 10);
                var range = new[] { graph.First(), graph.Last() };
                var algorithm = factory.Create(range);

                var path = algorithm.FindPath();

                Assert.IsTrue(path.Count > 0, wrongPathMsg);
            }
        }
    }
}
