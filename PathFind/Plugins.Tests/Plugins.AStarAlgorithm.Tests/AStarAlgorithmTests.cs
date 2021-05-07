using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

namespace Plugins.AStarALgorithm.Tests
{
    [TestFixture]
    public class AStarAlgorithmTests : AlgorithmTest
    {
        [Test]
        public override void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            base.FindPath_EndpointsBelongToGraph_ReturnsShortestPath();
        }

        [Test]
        public override void FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException()
        {
            base.FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException();
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