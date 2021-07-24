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
        [Test]
        [Timeout(DijkstraAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }

        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new DijkstraAlgorithm(graph, endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 200;
        }

        protected override int GetExpectedLength()
        {
            return 91;
        }
        #endregion
    }
}