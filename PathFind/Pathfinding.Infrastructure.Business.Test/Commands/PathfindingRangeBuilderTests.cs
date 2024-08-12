using Pathfinding.Infrastructure.Business.Commands;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Infrastructure.Data.Pathfinding;
using Pathfinding.Infrastructure.Data.Extensions;
using Pathfinding.Service.Interface.Commands;

namespace Pathfinding.Infrastructure.Business.Test.Commands
{
    [TestFixture]
    internal sealed class PathfindingRangeBuilderTests
    {
        private readonly TestGraphAssemble assemble;
        private readonly PathfindingRangeBuilder<TestVertex> rangeBuilder;

        public PathfindingRangeBuilderTests()
        {
            assemble = new();
            // Order sensitive
            var includeCommands = new IPathfindingRangeCommand<TestVertex>[]
            {
                new ReplaceTransitIsolatedVertex<TestVertex>(),
                new IncludeSourceVertex<TestVertex>(),
                new ReplaceIsolatedSourceVertex<TestVertex>(),
                new IncludeTargetVertex<TestVertex>(),
                new ReplaceIsolatedTargetVertex<TestVertex>(),
                new IncludeTransitVertex<TestVertex>(),
            };
            // Order sensitive
            var excludeCommands = new IPathfindingRangeCommand<TestVertex>[]
            {
                new ExcludeSourceVertex<TestVertex>(),
                new ExcludeTargetVertex<TestVertex>(),
                new ExcludeTransitVertex<TestVertex>()
            };
            var range = new PathfindingRange<TestVertex>();
            rangeBuilder = new PathfindingRangeBuilder<TestVertex>(range, 
                includeCommands, excludeCommands);
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

            rangeBuilder.Include(expectedRange[0]);
            rangeBuilder.Include(expectedRange[5]);
            rangeBuilder.Include(expectedRange[1]);
            rangeBuilder.Include(expectedRange[2]);
            rangeBuilder.Include(expectedRange[3]);
            rangeBuilder.Include(expectedRange[4]);
            var range = rangeBuilder.Range;

            Assert.IsTrue(range.SequenceEqual(expectedRange), "Range was not as expected");
        }

        [Test]
        public void IncludeInRange_AddingSameVertex_ShouldIgnore()
        {
            var graph = assemble.AssembleGraph(new int[] { 5, 5 });
            var vertex1 = graph.Get(0, 0);
            var vertex2 = graph.Get(1, 1);
            var vertex3 = graph.Get(2, 2);
            var vertex4 = graph.Get(2, 3);
            var vertex6 = graph.Get(4, 4);
            var expectedRange = new List<TestVertex>()
            {
                vertex1,
                vertex2,
                vertex3,
                vertex4,
                vertex6
            };

            rangeBuilder.Include(vertex1);
            rangeBuilder.Include(vertex6);
            rangeBuilder.Include(vertex6);
            rangeBuilder.Include(vertex2);
            rangeBuilder.Include(vertex3);
            rangeBuilder.Include(vertex4);
            var range = rangeBuilder.Range;

            Assert.IsTrue(range.SequenceEqual(expectedRange), "Range was not as expected");
        }
    }
}
