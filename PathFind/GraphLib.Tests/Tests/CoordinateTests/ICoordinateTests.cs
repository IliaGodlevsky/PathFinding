using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using Moq;
using NUnit.Framework;

namespace GraphLib.Tests.Tests.CoordinateTests
{
    [TestFixture]
    public class ICoordinateTests
    {
        private Mock<ICoordinate> first;
        private Mock<ICoordinate> second;

        [SetUp]
        public void SetUp()
        {
            first = new Mock<ICoordinate>();
            second = new Mock<ICoordinate>();
        }

        [TestCase(new int[] { 6, 6 },           new int[] { 6, 6 },             ExpectedResult = true)]
        [TestCase(new int[] { 0, 0, 0 },        new int[] { 0, 0, 0 },          ExpectedResult = true)]
        [TestCase(new int[] { 10, 10, 10, 10 }, new int[] { 10, 10, 10, 10 },   ExpectedResult = true)]
        [TestCase(new int[] { 6, 7 },           new int[] { 5, 6 },             ExpectedResult = false)]
        [TestCase(new int[] { 0, 1, 0 },        new int[] { 1, 0, 2 },          ExpectedResult = false)]
        [TestCase(new int[] { 10, 9, 10, 10 },  new int[] { 8, 10, 10, 10 },    ExpectedResult = false)]
        [TestCase(new int[] { 0, 1, 0 },        new int[] { 1, 0 },             ExpectedResult = false)]
        [TestCase(new int[] { 10, 9 },          new int[] { 8, 10, 10, 10 },    ExpectedResult = false)]
        public bool IsEqual_ComparesCoordinatesWithVariousCoordinateValues_ReturnsValidCondition(
            int[] firstCoordinateValues, int[] secondCoordinateValues)
        {
            first = new Mock<ICoordinate>();
            second = new Mock<ICoordinate>();
            first.Setup(coordinate => coordinate.CoordinatesValues).Returns(firstCoordinateValues);
            second.Setup(coordinate => coordinate.CoordinatesValues).Returns(secondCoordinateValues);

            return first.Object.IsEqual(second.Object);
        }
    }
}
