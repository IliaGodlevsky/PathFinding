using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System;
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
        public virtual void FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException()
        {
            var graph = testgraph2DAssemble.AssembleGraph();
            var endPoints = new TestEndPoints(new NullVertex(), new NullVertex());

            var algorithm = CreateAlgorithm(graph, endPoints);

            Assert.Throws<ArgumentException>(() => algorithm.FindPath());
        }

        [Test]
        public virtual void FindPath_NullGraph_ReturnsEmptyPath()
        {
            var graph = new NullGraph();
            var endPoints = new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();

            Assert.AreEqual(default(int), graphPath.PathLength);
            Assert.AreEqual(default(double), graphPath.PathCost);
        }

        [TestCase(new int[] { 500 })]
        [TestCase(new int[] { 15, 50 })]
        [TestCase(new int[] { 8, 9, 10 })]
        [TestCase(new int[] { 1, 2, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        public void FindPath_GraphsWithoutObstacles_ReturnsNotEmptyPath(int[] dimensionSizes)
        {
            var graph = testGraphAssemble.AssembleGraph(0, dimensionSizes);
            var endPoints = new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();

            Assert.IsTrue(graphPath.PathLength > 0);
            Assert.IsTrue(graphPath.PathCost > 0);
        }

        protected abstract IAlgorithm CreateAlgorithm(IGraph graph, IIntermediateEndPoints endPoints);

        protected abstract int GetExpectedCost();
        protected abstract int GetExpectedLength();

        #endregion
    }
}