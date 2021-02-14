using GraphLib.Extensions;
using GraphLib.Interface;
using Moq;
using NUnit.Framework;

namespace GraphLib.Tests.Tests.CoordinateTests.ExtensionsTests
{
    [TestFixture]
    internal class IsWithinGraphTests
    {
        private Mock<IGraph> graphMock;

        [SetUp]
        public void SetUp()
        {
            graphMock = new Mock<IGraph>();
        }

        [TestCase(new int[] { 8, 16 }, new int[] { 10, 20 }, ExpectedResult = true)]
        [TestCase(new int[] { 33, 1, 100 }, new int[] { 60, 50, 150 }, ExpectedResult = true)]
        [TestCase(new int[] { 33, 1, 100, 50 }, new int[] { 60, 50, 150, 67 }, ExpectedResult = true)]
        [TestCase(new int[] { 11, 16 }, new int[] { 10, 20 }, ExpectedResult = false)]
        [TestCase(new int[] { 33, 52, 100 }, new int[] { 60, 50, 150 }, ExpectedResult = false)]
        [TestCase(new int[] { 33, 1, 100, 69 }, new int[] { 60, 50, 150, 67 }, ExpectedResult = false)]
        public bool IsWithinGraph_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidCondition(
            int[] coordinateValues,
            int[] graphDimensionSizes)
        {
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(graphDimensionSizes);

            return coordinateValues.IsWithinGraph(graphMock.Object);
        }
    }
}
