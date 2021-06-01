using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class CoordinateExtensionsTests
    {
        private Mock<IGraph> graphMock;
        private Mock<ICoordinate> coordinateMock;
        private Mock<ICoordinate> secondCoordinateMock;

        [SetUp]
        public void Setup()
        {
            graphMock = new Mock<IGraph>();
            coordinateMock = new Mock<ICoordinate>();
            secondCoordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(new[] { 6, 6 }, new[] { 6, 6 }, ExpectedResult = true)]
        [TestCase(new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 10, 10, 10, 10 }, new[] { 10, 10, 10, 10 }, ExpectedResult = true)]
        [TestCase(new[] { 6, 7 }, new[] { 5, 6 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 0 }, new[] { 1, 0, 2 }, ExpectedResult = false)]
        [TestCase(new[] { 10, 9, 10, 10 }, new[] { 8, 10, 10, 10 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 0 }, new[] { 1, 0 }, ExpectedResult = false)]
        [TestCase(new[] { 10, 9 }, new[] { 8, 10, 10, 10 }, ExpectedResult = false)]
        public bool IsEqual_ComparesCoordinatesWithVariousCoordinateValues_ReturnsValidCondition(
            int[] firstCoordinateValues, int[] secondCoordinateValues)
        {
            coordinateMock.Setup(c => c.CoordinatesValues).Returns(firstCoordinateValues);
            secondCoordinateMock.Setup(c => c.CoordinatesValues).Returns(secondCoordinateValues);

            return coordinateMock.Object.IsEqual(secondCoordinateMock.Object);
        }

        [Test]
        public void IsEqual_CoordinatesIsNull_ReturnsFalse()
        {
            ICoordinate first = null;
            ICoordinate second = null;

            bool isEqual = first.IsEqual(second);

            Assert.IsFalse(isEqual);
        }

        [TestCase(new[] { 8, 16 }, new[] { 10, 20 }, ExpectedResult = true)]
        [TestCase(new[] { 33, 1, 100 }, new[] { 60, 50, 150 }, ExpectedResult = true)]
        [TestCase(new[] { 33, 1, 100, 50 }, new[] { 60, 50, 150, 67 }, ExpectedResult = true)]
        [TestCase(new[] { 11, 16 }, new[] { 10, 20 }, ExpectedResult = false)]
        [TestCase(new[] { 33, 52, 100 }, new[] { 60, 50, 150 }, ExpectedResult = false)]
        [TestCase(new[] { 33, 1, 100, 69 }, new[] { 60, 50, 150, 67 }, ExpectedResult = false)]
        public bool IsWithinGraph_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidCondition(
            int[] coordinateValues,
            int[] graphDimensionSizes)
        {
            graphMock.Setup(g => g.DimensionsSizes).Returns(graphDimensionSizes);
            coordinateMock.Setup(c => c.CoordinatesValues).Returns(coordinateValues);

            return coordinateMock.Object.IsWithinGraph(graphMock.Object);
        }

        [TestCase(new[] { 33, 52, 100 })]
        [TestCase(new[] { 33, 1, 100, 69 })]
        public void IsWithinGraph_GraphIsNull_ThrowsArgumentNullException(int[] coordinateValues)
        {
            IGraph graph = null;
            coordinateMock.Setup(c => c.CoordinatesValues).Returns(coordinateValues);

            Assert.Throws<ArgumentNullException>(() => coordinateMock.Object.IsWithinGraph(graph));
        }
    }
}
