using Moq;
using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Algorithms.GraphPaths;
using Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders;
using Pathfinding.Infrastructure.Business.Test.Mock;
using Pathfinding.Shared.Primitives;
using System.Collections;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms
{
    [TestFixture]
    public class WaveAlgorithmsTests
    {
        [TestCaseSource(typeof(WaveAlgorithmDataProvider),
            nameof(WaveAlgorithmDataProvider.DijkstraDataProvider))]
        public void DijkstraAlgorithm_ValidRange_ShouldFind(
            IEnumerable<IVertex> range,
            IEnumerable<Coordinate> expected)
        {
            var algorithm = new DijkstraAlgorithm(range);

            var path = algorithm.FindPath();

            Assert.IsTrue(path.SequenceEqual(expected),
                "The found path is not as the expected one");
        }

        [Test]
        public void PathfindingAlgorithm_Disposed_ShouldThrow()
        {
            var algorithm = new TestPathfindingAlgorithm();
            algorithm.Dispose();

            Assert.Throws<ObjectDisposedException>(() => algorithm.FindPath());
        }
    }
}
