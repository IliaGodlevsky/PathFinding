using Algorithm.AlgorithmCreating;
using GraphLib.Graphs.Abstractions;
using Moq;
using NUnit.Framework;
using System.Collections;

namespace Algorithm.Tests.AlgorithmCreatingTesting
{
    [TestFixture]
    internal class AlgorithmCreatingTest
    {
        private Mock<IGraph> graphMock;

        [SetUp]
        public void AlgorithmCreatingTestSetUp()
        {
            graphMock = new Mock<IGraph>(MockBehavior.Loose);
        }

        [Test, TestCaseSource(nameof(AlgorithmKeys))]
        public void CreateAlgorithm_ValidKey_ReturnsAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.CreateAlgorithm(key, graphMock.Object);

            Assert.IsTrue(!algorithm.IsDefault);
        }

        [TestCase("Algorithm A")]
        [TestCase("A*")]
        [TestCase("Pathfinding algorithm")]
        public void CreateAlgorithm_InvalidKey_ReturnsDefaultAlgorithm(string key)
        {
            var algorithm = AlgorithmFactory.CreateAlgorithm(key, graphMock.Object);

            Assert.IsTrue(algorithm.IsDefault);
        }

        private static readonly IEnumerable AlgorithmKeys = AlgorithmFactory.AlgorithmsDescriptions;
    }
}
