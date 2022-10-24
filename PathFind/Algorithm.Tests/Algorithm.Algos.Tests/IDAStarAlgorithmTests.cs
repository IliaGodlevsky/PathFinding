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
        public override void FindPath_PathfindingRangeBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_PathfindingRangeBelongToGraph_ReturnsShortestPath();
        }

        protected override IAlgorithm<IGraphPath> CreateAlgorithm(IPathfindingRange endPoints)
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