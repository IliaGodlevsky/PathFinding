using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;
using Pathfinding.Service.Interface.Extensions;
using Pathfinding.Shared.Extensions;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Commands
{
    [TestFixture, UnitTest]
    public sealed class PathfindingRangeCommandsTests
    {
        private readonly TestGraphAssemble assemble;
        private TestPathfindingRange range;
        private readonly IPathfindingRangeCommand<TestVertex>[] commands;

        public PathfindingRangeCommandsTests()
        {
            assemble = new();
            commands = new IPathfindingRangeCommand<TestVertex>[]
            {
                //new ExcludeTargetVertex<TestVertex>(),
                new IncludeSourceVertex<TestVertex>(),
                new ReplaceTransitIsolatedVertex<TestVertex>(),
                new IncludeTransitVertex<TestVertex>(),
                new ReplaceIsolatedSourceVertex<TestVertex>(),
                //new ExcludeSourceVertex<TestVertex>(),
                new IncludeTargetVertex<TestVertex>(),
                new ReplaceIsolatedTargetVertex<TestVertex>(),
                //new ExcludeTransitVertex<TestVertex>()
            }.OrderByOrderAttribute().ToArray();
        }

        [SetUp]
        public void Setup()
        {
            range = new TestPathfindingRange();
        }

        [Test]
        public void Include_VerticesWithNeighbours_ShouldCompositeValidRange()
        {
            var graph = assemble.AssembleGraph(new int[] { 5, 5 });
            var expectedRange = new List<TestVertex>()
            {
                graph.Get(0, 0),
                graph.Get(1, 1),
                graph.Get(2, 2),
                graph.Get(2, 3),
                graph.Get(3, 3),
                graph.Get(4, 4)
            };

            commands.ExecuteFirst(range, expectedRange[0]);
            commands.ExecuteFirst(range, expectedRange[5]);
            commands.ExecuteFirst(range, expectedRange[1]);
            commands.ExecuteFirst(range, expectedRange[2]);
            commands.ExecuteFirst(range, expectedRange[3]);
            commands.ExecuteFirst(range, expectedRange[4]);

            Assert.That(range.SequenceEqual(expectedRange), Is.True,
                "Range was not as expected");
        }

        [Test]
        public void IncludeInRange_AddingSameVertex_ShouldIgnore()
        {
            var graph = assemble.AssembleGraph(new int[] { 5, 5 });
            var vertex1 = graph.Get(0, 0);
            var vertex2 = graph.Get(1, 1);
            var vertex3 = graph.Get(2, 2);
            var vertex4 = graph.Get(2, 3);
            var vertex5 = graph.Get(4, 4);
            var expectedRange = new List<TestVertex>()
            {
                vertex1,
                vertex2,
                vertex3,
                vertex4,
                vertex5
            };
            commands.ExecuteFirst(range, vertex1);
            commands.ExecuteFirst(range, vertex5);
            commands.ExecuteFirst(range, vertex5);
            commands.ExecuteFirst(range, vertex2);
            commands.ExecuteFirst(range, vertex3);
            commands.ExecuteFirst(range, vertex4);

            Assert.That(range.SequenceEqual(expectedRange), Is.True,
                "Range was not as expected");
        }
    }
}
