using GraphLib.Common.NullObjects;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using GraphLib.Realizations.Tests.Factories;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphAssemblerTests
    {
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly IGraphFactory graphFactory;
        private readonly IGraphAssembler graphAssembler;

        public GraphAssemblerTests()
        {
            coordinateFactory = new TestCoordinateFactory();
            vertexFactory = new TestVertexFactory();
            costFactory = new TestCostFactory();
            graphFactory = new TestGraphFactory();
            graphAssembler = new GraphAssembler(vertexFactory,
                coordinateFactory, graphFactory, costFactory);
        }

        [TestCase(15, new int[] { 15 })]
        [TestCase(10, new int[] { 10, 15 })]
        [TestCase(10, new int[] { 7, 12, 10 })]
        [TestCase(33, new int[] { 7, 12, 10, 4 })]
        public void AssembleGraph_ReturnsNotNullGraph(int obstaclePercent, int[] dimensionSizes)
        {
            var graph = graphAssembler.AssembleGraph(obstaclePercent, dimensionSizes);

            Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensionSizes));
            Assert.IsFalse(graph is NullGraph);
        }
    }
}
