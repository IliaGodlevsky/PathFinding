using Algorithm.Interfaces;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
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
        private readonly ICoordinateRadarFactory radarFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly IGraphFactory graphFactory;
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IGraphAssemble graphAssemble;
        private readonly TestGraphAssemble testAssemble;

        public AlgorithmTest()
        {
            radarFactory = new CoordinateAroundRadarFactory();
            graphFactory = new TestGraphFactory();
            vertexFactory = new TestVertexFactory();
            costFactory = new TestVertexCostFactory();
            coordinateFactory = new TestCoordinateFactory();

            graphAssemble = new GraphAssemble(vertexFactory,
                coordinateFactory,
                graphFactory,
                costFactory,
                radarFactory);

            testAssemble = new TestGraphAssemble(graphAssemble);
        }

        #region Test Methods

        [Test]
        public virtual void FindPath_EndpointsBelongToGraph_ReturnsShortestPath()
        {
            int expectedLength = GetExpectedLength();
            int expectedCost = GetExpectedCost();
            var graph = testAssemble.AssembleGraph(0, Constants.Width, Constants.Length);
            var start = graph.Vertices.First();
            var end = graph.Vertices.Last();
            var endPoints = new TestEndPoints(start, end);
            var algorithm = CreateAlgorithm(graph, endPoints);

            var graphPath = algorithm.FindPath();
            var realLength = graphPath.Path.Count();
            var realCost = graphPath.PathCost;

            Assert.AreEqual(realLength, expectedLength);
            Assert.AreEqual(realCost, expectedCost);
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