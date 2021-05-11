using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.AStarMOdified.Tests
{
    [TestFixture]
    public class AStarModifiedTests : AlgorithmTest
    {
        private const int AStarModifiedAlgorithmTimeoutToFinishPathfinding = 1000;

        [Test]
        [Timeout(AStarModifiedAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }

        protected override IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints)
        {
            return new AStarModified.AStarModified(graph, endPoints);
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