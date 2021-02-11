using Algorithm.Interface;
using Algorithm.Tests.TestsInfrastructure;
using GraphLib.Extensions;
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

        [SetUp]
        public void SetUp()
        {
            endpointsMock = new Mock<IEndPoints>();
        }

        [TestCaseSource(
            typeof(TestCasesFactory),
            nameof(TestCasesFactory.AlgorithmWithGraphsTestCases))]
        public void FindPath_NotNullGraph_Success(IGraph graph, 
            IAlgorithm algorithm)
        {
            endpointsMock.Setup(end => end.Start).Returns(graph.First());
            endpointsMock.Setup(end => end.End).Returns(graph.Last());
            algorithm.Graph = graph;

            var path = algorithm.FindPath(endpointsMock.Object);

            Assert.IsTrue(path.IsExtracted());
        }

        [TestCaseSource(
            typeof(TestCasesFactory),
            nameof(TestCasesFactory.AlgorithmsTestCases))]
        public void FindPath_NullGraph_ThrowsException(IAlgorithm algorithm)
        {
            endpointsMock.Setup(end => end.Start).Returns(new DefaultVertex());
            endpointsMock.Setup(end => end.End).Returns(new DefaultVertex());

            Assert.Throws<Exception>(() => algorithm.FindPath(endpointsMock.Object));
        }
    }
}
