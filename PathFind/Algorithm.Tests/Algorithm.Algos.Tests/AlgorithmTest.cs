using Algorithm.Infrastructure;
using Algorithm.Interfaces;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NullObject.Extensions;
using NUnit.Framework;
using System.Linq;

namespace Algorithm.Algos.Tests
{
    [TestFixture]
    public abstract class AlgorithmTest
    {
        private readonly TestGraph2DAssemble testgraph2DAssemble;
        private readonly TestGraphAssemble testGraphAssemble;

        public AlgorithmTest()
        {
            testgraph2DAssemble = new TestGraph2DAssemble();
            testGraphAssemble = new TestGraphAssemble();
        }

        #region Test Methods

        [Test]
        public virtual void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            var graph = testgraph2DAssemble.AssembleGraph();
            var endPoints = new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();

            Assert.AreEqual(GetExpectedLength(), graphPath.PathLength);
            Assert.AreEqual(GetExpectedCost(), graphPath.PathCost);
        }

        [Test]
        public virtual void FindPath_EndPointsDoesntBelongToGraph_TrowsEndPointsNotFromCurrentGraphException()
        {
            var graph = testgraph2DAssemble.AssembleGraph();
            var endPoints = new TestEndPoints(NullVertex.Instance, NullVertex.Instance);

            var algorithm = CreateAlgorithm(graph, endPoints);

            Assert.Throws<EndPointsNotFromCurrentGraphException>(() => algorithm.FindPath());
        }

        [Test]
        public virtual void FindPath_NullGraph_ReturnsNullGraph()
        {
            var graph = NullGraph.Instance;
            var source = graph.FirstOrNullVertex();
            var target = graph.LastOrNullVertex();
            var endPoints = new TestEndPoints(source, target);

            var algorithm = CreateAlgorithm(graph, endPoints);
            var path = algorithm.FindPath();

            Assert.IsTrue(path.IsNull());
        }

        [TestCase(new int[] { 500 })]
        [TestCase(new int[] { 15, 50 })]
        [TestCase(new int[] { 8, 9, 10 })]
        [TestCase(new int[] { 1, 2, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        public void FindPath_GraphsWithoutObstacles_ReturnsNotEmptyPath(int[] dimensionSizes)
        {
            var graph = testGraphAssemble.AssembleGraph(0, dimensionSizes);
            var source = graph.FirstOrNullVertex();
            var target = graph.LastOrNullVertex();
            var endPoints = new TestEndPoints(source, target);
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();

            Assert.IsFalse(graph.IsNull());
        }

        protected abstract IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints);

        protected abstract int GetExpectedCost();
        protected abstract int GetExpectedLength();

        #endregion
    }
}