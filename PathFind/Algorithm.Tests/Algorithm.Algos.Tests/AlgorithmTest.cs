using Algorithm.Interfaces;
using Algorithm.NullRealizations;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.NullRealizations;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NullObject.Extensions;
using NUnit.Framework;
using System.Linq;

namespace Algorithm.Algos.Tests
{
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
            var algorithm = CreateAlgorithm(endPoints);

            var graphPath = algorithm.FindPath();

            Assert.AreEqual(GetExpectedLength(), graphPath.Length);
            Assert.AreEqual(GetExpectedCost(), graphPath.Cost);
        }

        [TestCase(TestName = "Finding path using NullEndPoints returns NullGraphPath")]
        public virtual void FindPath_NullEndPoints_ReturnsNullGraphPath()
        {
            var algorithm = CreateAlgorithm(NullEndPoints.Instance);

            var graphPath = algorithm.FindPath();

            Assert.AreSame(NullGraphPath.Instance, graphPath);
        }

        [TestCase(new int[] { 500 }, TestName = "Finding path in one dimensional graph with 500 vertices")]
        [TestCase(new int[] { 15, 50 }, TestName = "Finding path in two dimensional graph (15x50)")]
        [TestCase(new int[] { 8, 9, 10 }, TestName = "Finding path in three dimensional graph (8x9x10)")]
        [TestCase(new int[] { 1, 2, 4, 5 }, TestName = "Finding path in four dimensional graph (1x2x3x4)")]
        [TestCase(new int[] { 1, 2, 3, 4, 5 }, TestName = "Finding path in five dimensional graph (1x2x3x4x5)")]
        public void FindPath_GraphsWithoutObstacles_ReturnsNotEmptyPath(int[] dimensionSizes)
        {
            var graph = testGraphAssemble.AssembleGraph(0, dimensionSizes);
            var source = graph.FirstOrNullVertex();
            var target = graph.LastOrNullVertex();
            var endPoints = new TestEndPoints(source, target);
            var algorithm = CreateAlgorithm(endPoints);

            var graphPath = algorithm.FindPath();

            Assert.IsFalse(graph.IsNull());
        }

        protected abstract IAlgorithm CreateAlgorithm(IEndPoints endPoints);

        protected abstract int GetExpectedCost();
        protected abstract int GetExpectedLength();

        #endregion
    }
}