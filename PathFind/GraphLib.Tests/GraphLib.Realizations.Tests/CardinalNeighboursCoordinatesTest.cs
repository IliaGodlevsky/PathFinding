using GraphLib.Interfaces;
using GraphLib.Realizations.NeighboursCoordinates;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class CardinalNeighboursCoordinatesTest
    {
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            coordinateMock = new Mock<ICoordinate>();
        }

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
            int dimensions = coordinateValues.Length;
            int expectedResult = 2 * dimensions;
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalNeighboursCoordinates(coordinateMock.Object);

            var environment = coordinateEnvironment.Coordinates.ToArray();

            Assert.AreEqual(expectedResult, environment.Length);
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
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalNeighboursCoordinates(coordinateMock.Object);

            var environment = coordinateEnvironment.Coordinates;
            bool hasSelf = environment.Any(values => values.Equals(coordinateMock.Object));

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
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalNeighboursCoordinates(coordinateMock.Object);

            var environment = coordinateEnvironment.Coordinates.ToArray();

            Assert.IsTrue(environment.Distinct().Count() == environment.Count());
        }
    }
}