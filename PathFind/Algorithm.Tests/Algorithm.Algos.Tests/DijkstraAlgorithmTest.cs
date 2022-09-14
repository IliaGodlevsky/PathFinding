using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public class DijkstraAlgorithmTest : AlgorithmTest
    {
        private const int DijkstraAlgorithmTimeoutToFinishPathfinding = 2100;

        #region Test Methods
        [TestCase(TestName = "Finding path with valid endpoints within 2100 milliseconds")]
        [Timeout(DijkstraAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }

        protected override IAlgorithm<IGraphPath> CreateAlgorithm(IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 67;
        }

        protected override int GetExpectedLength()
        {
            return 31;
        }
        #endregion
    }
}