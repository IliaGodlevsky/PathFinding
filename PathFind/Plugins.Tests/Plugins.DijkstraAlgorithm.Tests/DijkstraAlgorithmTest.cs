using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest;

using DijkstrasAlgorithm = Plugins.DijkstraALgorithm.DijkstraAlgorithm;

namespace Plugins.DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class DijkstraAlgorithmTest : AlgorithmTest
    {
        #region Test Methods

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
            return new DijkstrasAlgorithm(graph, endPoints);
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