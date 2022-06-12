using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [TestCase(new[] { 1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1 }, ExpectedResult = true)]
        [TestCase(new[] { -1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, -1 }, ExpectedResult = true)]
        [TestCase(new[] { -1, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 1, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { -1, 1 }, ExpectedResult = false)]
        public bool IsCardinal_Various2DCoordinates_ReturnsValidResult(int[] coordinates)
        {
            int[] centralCoordinate = new[] { 0, 0 };

            return centralCoordinate.IsCardinal(coordinates);
        }

        [TestCase(new[] { 1, 1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { -1, -1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, -1, 0 }, ExpectedResult = true)]
        [TestCase(new[] { -1, 0, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 1, 0, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 1, -1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { -1, 1, 1 }, ExpectedResult = false)]
        public bool IsCardinal_Various3DCoordinates_ReturnsValidResult(int[] coordinates)
        {
            int[] centralCoordinate = new[] { 0, 0, 0 };

            return centralCoordinate.IsCardinal(coordinates);
        }

        [TestCase(new[] { 1, 1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 1, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 0 }, ExpectedResult = false)]
        public bool IsCardinal_VariousCoordinatesWithDifferentDimensionSizes_ReturnsFalse(int[] coordinates)
        {
            int[] centralCoordinate = new[] { 0, 0 };

            return centralCoordinate.IsCardinal(coordinates);
        }

        [TestCase(new[] { 11, 12 })]
        [TestCase(new[] { 11, 12, 13 })]
        public void ToCoordinates_IndexIsOutOfRange_ThrowsArgumentOurOfRangeException(int[] dimensionSizes)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => dimensionSizes.ToCoordinates(-1));
        }

        [TestCase(new[] { 11, 12 })]
        public void ToCoordinates_IndexIsInRange_ReturnCoordinates(int[] dimensionSizes)
        {
            var expectedCoordinates = new[] { 7, 3 };
            int index = 40;

            var coordinates = dimensionSizes.ToCoordinates(index);

            Assert.IsTrue(coordinates.SequenceEqual(expectedCoordinates));
        }
    }
}
