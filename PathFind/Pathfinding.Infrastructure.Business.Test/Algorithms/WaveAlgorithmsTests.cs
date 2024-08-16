using Pathfinding.Domain.Interface;
using Pathfinding.Infrastructure.Business.Algorithms;
using Pathfinding.Infrastructure.Business.Test.Algorithms.DataProviders;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Business.Test.Algorithms
{
    [TestFixture]
    public class WaveAlgorithmsTests
    {
        [TestCaseSource(typeof(WaveAlgorithmDataProvider), 
            nameof(WaveAlgorithmDataProvider.DijstraDataProvider))]
        public void DijkstraAlgorithm_ValidRange_ShouldFind(
            IEnumerable<IVertex> range, 
            IEnumerable<Coordinate> expected)
        {
            var algorithm = new DijkstraAlgorithm(range);

            var path = algorithm.FindPath();

            Assert.IsTrue(path.SequenceEqual(expected),
                "The found path is not as the expected one");
        }
    }
}
