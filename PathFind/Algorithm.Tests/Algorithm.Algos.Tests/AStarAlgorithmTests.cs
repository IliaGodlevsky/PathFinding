using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public class AStarAlgorithmTests : AlgorithmTest
    {
        private const int AStarAlgorithmTimeoutToFinishPathfinding = 1900;

        [TestCase(TestName = "Finding path with valid endpoints within 1900 milliseconds")]
        [Timeout(AStarAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }
        protected override IAlgorithm CreateAlgorithm(IEndPoints endPoints)
        {
            return new AStarAlgorithm(endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 67;
        }

        protected override int GetExpectedLength()
        {
            return 31;
        }
    }
}