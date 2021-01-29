using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Tests.Tests.CoordinateTests
{
    [TestFixture]
    public class Coordinate2DTests
    {
        private const int NeighboursOfCoordinate2D = 8;
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            coordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(1)]
        [TestCase(1, 2, 3)]
        [TestCase()]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsArgumentOutOfRangeException(params int[] coordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate2D(coordinates));
        }

        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        public void Constructor_ValidCoordinateValuesNumber_ReturnsInstanceOfCoordinate2DClass(int x, int y)
        {
            ICoordinate coordinate = new Coordinate2D(x, y);

            Assert.IsInstanceOf(typeof(Coordinate2D), coordinate);
        }

        [Test]
        public void Equals_IntAsArgument_ThrowsArgumentException()
        {
            ICoordinate coordinate = new Coordinate2D(0, 0);

            Assert.Throws<ArgumentException>(() => coordinate.Equals(default(int)));
        }

        [TestCase(6, 6, 6, 6)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(10, 10, 10, 10)]
        public void Equals_ComparesEqualCoordinates2D_ReturnsTrue(int x1, int y1, int x2, int y2)
        {
            ICoordinate first = new Coordinate2D(x1, y1);
            ICoordinate second = new Coordinate2D(x2, y2);

            var areEqual = first.Equals(second);

            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Equals_ComparesNotEqualCoordinates2D_ReturnsFalse()
        {
            ICoordinate first = new Coordinate2D(5, 5);
            ICoordinate second = new Coordinate2D(6, 6);

            var areEqual = first.Equals(second);

            Assert.IsFalse(areEqual);
        }


        [TestCase(3, 13, 10, 15)]
        [TestCase(3)]
        [TestCase(3, 3, 3)]
        [TestCase()]
        [TestCase(7, 11, 1, 15, 77)]
        public void Equals_Compares2DAndNot2DCoordinate_ReturnsFalse(params int[] coordinates)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinates);

            ICoordinate coordinate2D = new Coordinate2D(3, 3);

            var areEqual = coordinate2D.Equals(coordinateMock.Object);

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Clone_Coordinate2D_ReturnsANewInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate2D(5, 5);

            var clone = coordinate.Clone();

            Assert.IsInstanceOf(typeof(Coordinate2D), clone);
            Assert.IsTrue(clone.Equals(coordinate));
            Assert.AreNotSame(clone, coordinate);
        }
    }
}