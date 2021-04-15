using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class CoordinateExtensionsTests
    {
        private Mock<ICoordinate> first;
        private Mock<ICoordinate> second;

        [SetUp]
        public void SetUp()
        {
            first = new Mock<ICoordinate>();
            second = new Mock<ICoordinate>();
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
            first = new Mock<ICoordinate>();
            second = new Mock<ICoordinate>();
            first.Setup(coordinate => coordinate.CoordinatesValues).Returns(firstCoordinateValues);
            second.Setup(coordinate => coordinate.CoordinatesValues).Returns(secondCoordinateValues);

            return first.Object.IsEqual(second.Object);
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
