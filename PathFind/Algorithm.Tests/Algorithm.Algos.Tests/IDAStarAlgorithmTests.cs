using Algorithm.Algos.Algos;
using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public class IDAStarAlgorithmTests : AlgorithmTest
    {
        private const int AStarModifiedAlgorithmTimeoutToFinishPathfinding = 1000;

        [TestCase(TestName = "Finding path with valid endpoints within 1000 milliseconds")]
        [Timeout(AStarModifiedAlgorithmTimeoutToFinishPathfinding)]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }

        protected override IAlgorithm<IGraphPath> CreateAlgorithm(IEndPoints endPoints)
        {
            return new IDAStarAlgorithm(endPoints);
        }

        protected override int GetExpectedCost()
        {
            return 70;
        }

        protected override int GetExpectedLength()
        {
            return 28;
        }
    }
}