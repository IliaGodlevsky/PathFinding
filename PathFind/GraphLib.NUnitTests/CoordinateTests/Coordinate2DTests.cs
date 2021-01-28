using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.NUnitTests
{
    [TestFixture]
    public class Coordinate2DTests
    {
        private const int NeighboursOfCoordinate2D = 8;

        [TestCase(1)]
        [TestCase(1,2,3)]
        [TestCase()]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsArgumentOutOfRangeException(params int[] coordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate2D(coordinates));
        }

        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        public void Constructor_TwoCoordinateValues_ReturnsInstanceOfCoordinate2DClass(int x, int y)
        {
            ICoordinate coordinate = new Coordinate2D(x, y);

            Assert.IsInstanceOf(typeof(Coordinate2D), coordinate);
        }

        [TestCase(2, 2)]
        [TestCase(3, 4)]
        [TestCase(4, 5)]
        public void Environment_TwoCoordinateValues_Return8Coordinates(int x, int y)
        {
            ICoordinate coordinate = new Coordinate2D(x, y);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Count() == NeighboursOfCoordinate2D);
        }

        [TestCase(12, 2)]
        [TestCase(3, 34)]
        [TestCase(4, 57)]
        public void Environment_TwoCoordinateValues_ReturnUniqueCoordinates(int x, int y)
        {
            ICoordinate coordinate = new Coordinate2D(x, y);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Distinct().Count() == NeighboursOfCoordinate2D);
        }

        [TestCase(0, 12)]
        [TestCase(100, 4)]
        [TestCase(40, 5)]
        public void Environment_Coordinate2DX5Y5_ReturnCoordinatesWithoutSelf(int x, int y)
        {
            ICoordinate coordinate = new Coordinate2D(x, y);

            var environment = coordinate.Environment;

            Assert.IsFalse(environment.Contains(coordinate));
        }

        [Test]
        public void Equals_ComparesIntAndCoordinate2D_ThrowsArgumentException()
        {
            ICoordinate coordinate = new Coordinate2D(0, 0);

            Assert.Throws<ArgumentException>(() => coordinate.Equals(5));
        }

        [TestCase(6, 6, 6, 6)]
        [TestCase(0, 0, 0, 0)]
        [TestCase(10, 10, 10, 10)]
        public void Equals_ComparesEqualCoordinate2D_ReturnsTrue(int x1, int y1, int x2, int y2)
        {
            ICoordinate first = new Coordinate2D(x1, y1);
            ICoordinate second = new Coordinate2D(x2, y2);

            var areEqual = first.Equals(second);

            Assert.IsTrue(areEqual);
        }

        [Test]
        public void Equals_ComparesNotEqualCoordinate2D_ReturnsFalse()
        {
            ICoordinate first = new Coordinate2D(5, 5);
            ICoordinate second = new Coordinate2D(6, 6);

            var areEqual = first.Equals(second);

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Equals_Compares2DAnd3DCoordinates_ReturnsFalse()
        {
            ICoordinate coordinate2D = new Coordinate2D(3, 3);
            ICoordinate coordinate3D = new Coordinate3D(3, 3, 3);

            var areEqual = coordinate2D.Equals(coordinate3D);

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