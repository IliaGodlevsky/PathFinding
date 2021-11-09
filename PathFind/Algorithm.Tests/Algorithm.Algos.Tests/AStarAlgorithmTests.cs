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

        [Test]
        [Timeout(AStarAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints)
        {
            return new AStarAlgorithm(graph, endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 200;
        }

        protected override int GetExpectedLength()
        {
            return 91;
        }
    }
}