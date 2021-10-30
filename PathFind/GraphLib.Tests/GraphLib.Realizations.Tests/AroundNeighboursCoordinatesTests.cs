using Common.Extensions;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.NeighboursCoordinates;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class AroundNeighboursCoordinatesTests
    {
        [TestCase(new int[] { })]
        [TestCase(new[] { 2 })]
        [TestCase(new[] { 2, 3 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnValidNumberOfNeighbours(int[] coordinateValues)
        {
            const int Self = 1;
            int dimensions = coordinateValues.Length;
            ulong expectedResult = 3.Pow(dimensions) - Self;
            var coordinate = new TestCoordinate(coordinateValues);
            var neighboursCoordinates = new AroundNeighboursCoordinates(coordinate);

            var coordinates = neighboursCoordinates.Coordinates;

            Assert.AreEqual(expectedResult, coordinates.LongCount());
        }

        [TestCase(new int[] { })]
        [TestCase(new[] { 2 })]
        [TestCase(new[] { 2, 3 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Coordinates_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            var coordinate = new TestCoordinate(coordinateValues);
            var neighboursCoordinates = new AroundNeighboursCoordinates(coordinate);

            bool hasSelf = neighboursCoordinates.Coordinates.Any(values => values.Equals(coordinate));

            Assert.IsFalse(hasSelf);
        }

        [TestCase(new int[] { })]
        [TestCase(new[] { 3 })]
        [TestCase(new[] { 1, 7 })]
        [TestCase(new[] { 100, 25, 33 })]
        [TestCase(new[] { 20, 13, 34, 58 })]
        [TestCase(new[] { 12, 32, 44, 51, 69 })]
        [TestCase(new[] { 23, 34, 45, 56, 67, 78 })]
        [TestCase(new[] { 201, 33, 84, 15, 16, 73, 81 })]
        [TestCase(new[] { 201, 33, 84, 15, 16, 73, 81, 11 })]
        public void Coordinates_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            var coordinate = new TestCoordinate(coordinateValues);
            var neighboursCoordinates = new AroundNeighboursCoordinates(coordinate);

            var coordinates = neighboursCoordinates.Coordinates;

            Assert.IsTrue(coordinates.Distinct().Count() == coordinates.Count);
        }

        [Test]
        public void Coordinates_NullCoordinate_ReturnEmptyEnvironment()
        {
            var coordinateEnvironment = new AroundNeighboursCoordinates(NullCoordinate.Instance);

            var environment = coordinateEnvironment.Coordinates.ToArray();

            Assert.IsFalse(environment.Any());
        }
    }
}
