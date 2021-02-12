using Algorithm.Interface;
using Algorithm.Tests.TestsInfrastructure.Factories;
using GraphLib.Extensions;
using GraphLib.Factories;
using GraphLib.Interface;
using GraphLib.NullObjects;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace Algorithm.Tests.Tests.AlgorithmTesting
{
    [TestFixture]
    internal class AlgorithmTests
    {
        private Mock<IEndPoints> endpointsMock;

        private readonly IVertexCostFactory costFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IGraphFactory graphFactory;

        private readonly GraphAssembler graphAssembler;

        public AlgorithmTests()
        {
            endpointsMock = new Mock<IEndPoints>();
            vertexFactory = new TestVertexFactory();
            costFactory = new TestCostFactory();
            graphFactory = new TestGraphFactory();
            coordinateFactory = new TestCoordinateFactory();
            graphAssembler = new GraphAssembler(
                vertexFactory, 
                coordinateFactory, 
                graphFactory, 
                costFactory);
        }

        [SetUp]
        public void Setup()
        {
            endpointsMock = new Mock<IEndPoints>();
        }

        [TestCaseSource(typeof(TestCasesFactory), nameof(TestCasesFactory.AlgorithmsWithGraphParamsTestCases))]
        public void FindPath_NotNullGraph_Success(IAlgorithm algorithm, int[] dimensionsSizes)
        {
            var graph = graphAssembler.AssembleGraph(0, dimensionsSizes);
            endpointsMock.Setup(end => end.Start).Returns(graph.Vertices.First());
            endpointsMock.Setup(end => end.End).Returns(graph.Vertices.Last());
            algorithm.Graph = graph;

            var path = algorithm.FindPath(endpointsMock.Object);

            Assert.IsTrue(path.IsExtracted());
        }

        [TestCaseSource(typeof(TestCasesFactory), nameof(TestCasesFactory.AlgorithmsTestCases))]
        public void FindPath_NullGraph_ThrowsException(IAlgorithm algorithm)
        {
            endpointsMock.Setup(end => end.Start).Returns(new DefaultVertex());
            endpointsMock.Setup(end => end.End).Returns(new DefaultVertex());

            Assert.Throws<Exception>(() => algorithm.FindPath(endpointsMock.Object));
        }
    }
}
