using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders;
using Pathfinding.Infrastructure.Business.Test.TestRealizations;
using Pathfinding.Shared.Primitives;
using Pathfinding.TestUtils.Attributes;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms
{
    [TestFixture, UnitTest]
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

            Assert.That(path.SequenceEqual(expected), Is.True,
                "The found path is not as the expected one");
        }
    }
}
