using GraphLib.Coordinates.Abstractions;
using GraphLib.Coordinates.Infrastructure;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Tests.Tests.CoordinateTests
{
    [TestFixture]
    public class CoordinateEnvironmentTests
    {
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            coordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(new int[] { 2,},                      ExpectedResult = 2)]
        [TestCase(new int[] { 2, 3 },                   ExpectedResult = 8)]
        [TestCase(new int[] { 2, 3, 4 },                ExpectedResult = 26)]
        [TestCase(new int[] { 2, 3, 4, 5 },             ExpectedResult = 80)]
        [TestCase(new int[] { 2, 3, 4, 5, 6 },          ExpectedResult = 242)]
        [TestCase(new int[] { 2, 3, 4, 5, 6, 7 },       ExpectedResult = 728)]
        [TestCase(new int[] { 2, 3, 4, 5, 6, 7, 8 },    ExpectedResult = 2186)]
        public int GetEnvironment_CoordinatesWithVariousDimensionsNumber_ReturnValidNumberOfNeighbours(int [] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CoordinateEnvironment(coordinateMock.Object);

            var environment = coordinateEnvironment.GetEnvironment();

            return environment.Count();
        }

        [TestCase(new int[] { 2,})]
        [TestCase(new int[] { 2, 3 })]
        [TestCase(new int[] { 2, 3, 4 })]
        [TestCase(new int[] { 2, 3, 4, 5 })]
        [TestCase(new int[] { 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { 2, 3, 4, 5, 6, 7 })]
        [TestCase(new int[] { 2, 3, 4, 5, 6, 7, 8 })]
        public void GetEnvironment_CoordinatesWithVariousDimensionsNumber_ReturnNeighboursWithoutSelf(int[] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            var coordinateEnvironment = new CoordinateEnvironment(coordinateMock.Object);

            var environment = coordinateEnvironment.GetEnvironment();
            bool hasSelf = environment.Any(values => values.SequenceEqual(coordinateValues));

            Assert.IsFalse(hasSelf);
        }

        [TestCase(new int[] { 3, })]
        [TestCase(new int[] { 1, 7 })]
        [TestCase(new int[] { 100, 25, 33 })]
        [TestCase(new int[] { 20, 13, 34, 58 })]
        [TestCase(new int[] { 12, 32, 44, 51, 69 })]
        [TestCase(new int[] { 23, 34, 45, 56, 67, 78 })]
        [TestCase(new int[] { 201, 33, 84, 15, 16, 73, 81 })]
        public void GetEnvironment_CoordinatesWithVariousDimensionsCount_ReturnUniqueCoordinates(int[] coordinateValues)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            CoordinateEnvironment coordinateEnvironment = new CoordinateEnvironment(coordinateMock.Object);

            var environment = coordinateEnvironment.GetEnvironment();

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
