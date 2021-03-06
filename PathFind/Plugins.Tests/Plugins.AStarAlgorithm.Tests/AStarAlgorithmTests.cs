using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.AStarALgorithm.Tests
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
        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new AStarAlgorithm.AStarAlgorithm(graph, endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 229;
        }

        protected override int GetExpectedLength()
        {
            return 79;
        }
    }
}