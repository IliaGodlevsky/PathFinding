using NUnit.Framework;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [TestCase(new[] { 1, 1 },  ExpectedResult = false)]
        [TestCase(new[] { 0, 1 },  ExpectedResult = true)]
        [TestCase(new[] {-1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, -1 }, ExpectedResult = true)]
        [TestCase(new[] { -1, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 1, 0 },  ExpectedResult = true)]
        [TestCase(new[] { 1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { -1, 1 }, ExpectedResult = false)]
        public bool IsCardinal_Various2DCoordinates_ReturnsValidResult(int[] coordinates)
        {
            int[] centralCoordinate = new[] { 0, 0 };

            return centralCoordinate.IsCardinal(coordinates);
        }

        [TestCase(new[] { 1, 1, 1 },    ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 1 },    ExpectedResult = false)]
        [TestCase(new[] { -1, -1, -1 }, ExpectedResult = false)]
        [TestCase(new[] { 0, -1, 0 },   ExpectedResult = true)]
        [TestCase(new[] { -1, 0, 0 },   ExpectedResult = true)]
        [TestCase(new[] { 1, 0, 0 },    ExpectedResult = true)]
        [TestCase(new[] { 1, -1, 1 },   ExpectedResult = false)]
        [TestCase(new[] { -1, 1, 1 },   ExpectedResult = false)]
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

        [TestCase(new[] { 1, 2 }, ExpectedResult = false)]
        [TestCase(new[] { 2, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 3, 1 }, ExpectedResult = false)]
        [TestCase(new[] { 5, 5 }, ExpectedResult = false)]
        public bool IsCardinal_CoordinatesWithTooFarCoordinates_ReturnsFalse(int[] coordinates)
        {
            int[] centralCoordinate = new[] { 0, 0 };

            return centralCoordinate.IsCardinal(coordinates);
        }
    }
}
