using Algorithm.Interfaces;
using GraphLib.Interfaces;
using NUnit.Framework;
using Plugins.BaseAlgorithmUnitTest.Objects.Factories;
using Plugins.BaseAlgorithmUnitTest.Objects.TestObjects;
using System;
using System.Linq;

namespace Plugins.BaseAlgorithmUnitTest
{
    [TestFixture]
    public abstract class AlgorithmTest
    {
        private readonly TestGraphAssemble testAssemble;

        public AlgorithmTest()
        {
            testAssemble = new TestGraphAssemble();
        }

        #region Test Methods

        [Test]
        public virtual void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            var graph = testAssemble.AssembleGraph(0, Constants.Width, Constants.Length);
            var endPoints = new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();

            Assert.AreEqual(GetExpectedLength(), graphPath.Path.Count());
            Assert.AreEqual(GetExpectedCost(), graphPath.PathCost);
        }

        [Test]
        public virtual void FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException()
        {
            var graph = testAssemble.AssembleGraph(0, Constants.Width, Constants.Length);
            var endPoints = new TestEndPoints();

            var algorithm = CreateAlgorithm(graph, endPoints);

            Assert.Throws<ArgumentException>(() => algorithm.FindPath());
        }

        protected abstract IAlgorithm CreateAlgorithm(IGraph graph, IEndPoints endPoints);

        protected abstract int GetExpectedCost();
        protected abstract int GetExpectedLength();

        #endregion
    }
}