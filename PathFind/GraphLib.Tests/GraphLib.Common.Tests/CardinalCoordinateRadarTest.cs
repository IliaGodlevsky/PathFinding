using GraphLib.Common.CoordinateRadars;
using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Common.Tests
{
    [TestFixture]
    public class CardinalCoordinateAroundRadarTest
    {
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            coordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(new[] { 2 }, ExpectedResult = 2)]
        [TestCase(new[] { 2, 3 }, ExpectedResult = 4)]
        [TestCase(new[] { 2, 3, 4 }, ExpectedResult = 6)]
        [TestCase(new[] { 2, 3, 4, 5 }, ExpectedResult = 8)]
        [TestCase(new[] { 2, 3, 4, 5, 6 }, ExpectedResult = 10)]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 }, ExpectedResult = 12)]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 }, ExpectedResult = 14)]
        public int Environment_CoordinatesWithVariousDimensionsNumber_ReturnValidNumberOfNeighbours(int[] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalCoordinateAroundRadar(coordinateMock.Object);

            var environment = coordinateEnvironment.Environment;

            return environment.Count();
        }

        [TestCase(new[] { 2 })]
        [TestCase(new[] { 2, 3 })]
        [TestCase(new[] { 2, 3, 4 })]
        [TestCase(new[] { 2, 3, 4, 5 })]
        [TestCase(new[] { 2, 3, 4, 5, 6 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void Environment_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalCoordinateAroundRadar(coordinateMock.Object);

            var environment = coordinateEnvironment.Environment;
            bool hasSelf = environment.Any(values => values.SequenceEqual(coordinateValues));

            Assert.IsFalse(hasSelf);
        }

        [TestCase(new[] { 3 })]
        [TestCase(new[] { 1, 7 })]
        [TestCase(new[] { 100, 25, 33 })]
        [TestCase(new[] { 20, 13, 34, 58 })]
        [TestCase(new[] { 12, 32, 44, 51, 69 })]
        [TestCase(new[] { 23, 34, 45, 56, 67, 78 })]
        [TestCase(new[] { 201, 33, 84, 15, 16, 73, 81 })]
        public void Environment_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CardinalCoordinateAroundRadar(coordinateMock.Object);

            var environment = coordinateEnvironment.Environment.ToArray();

            Assert.IsTrue(environment.Select(CreateCoordinateMock).Distinct().Count() == environment.Count());
        }

        private Mock<ICoordinate> CreateCoordinateMock(int[] coordinates)
        {
            var mock = new Mock<ICoordinate>();
            mock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinates.ToArray());
            return mock;
        }
    }
}