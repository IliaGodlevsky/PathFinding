using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;

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
    }
}
