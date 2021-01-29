using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Tests.Tests.CoordinateTests
{
    [TestFixture]
    public class Coordinate3DTests
    {
        private const int NeighboursOfCoordinate3D = 26;

        [TestCase(1)]
        [TestCase(1, 2)]
        [TestCase()]
        [TestCase(1, 2, 3, 4)]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsArgumentOutOfRangeException(params int[] coordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate3D(coordinates));
        }

        [TestCase(2, 2, 1)]
        [TestCase(3, 4, 7)]
        [TestCase(4, 5, 15)]
        public void Constructor_ValidCoordinateValuesNumber_ReturnsInstanceOfCoordinate3DClass(int x, int y, int z)
        {
            ICoordinate coordinate = new Coordinate3D(x, y, z);

            Assert.IsInstanceOf(typeof(Coordinate3D), coordinate);
        }

        [TestCase(2, 2, 10)]
        [TestCase(30, 4, 70)]
        [TestCase(454, 5, 150)]
        public void Environment_ThreeCoordinateValues_Return26Coordinates(int x, int y, int z)
        {
            ICoordinate coordinate = new Coordinate3D(x, y, z);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Count() == NeighboursOfCoordinate3D);
        }

        [TestCase(12, 2, 12)]
        [TestCase(13, 4, 7)]
        [TestCase(4, 15, 15)]
        public void Environment_ThreeCoordinateValues_ReturnUniqueCoordinates(int x, int y, int z)
        {
            ICoordinate coordinate = new Coordinate3D(x, y, z);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Distinct().Count() == NeighboursOfCoordinate3D);
        }

        [TestCase(21, 2, 12)]
        [TestCase(34, 41, 77)]
        [TestCase(40, 5, 15)]
        public void Environment_ThreeCoordinateValues_Return26CoordinatesWithoutSelf(int x, int y, int z)
        {
            ICoordinate coordinate = new Coordinate3D(x, y, z);

            var environment = coordinate.Environment;

            Assert.IsFalse(environment.Contains(coordinate));
        }

        [Test]
        public void Equals_ComparesIntAndCoordinate3D_ThrowsArgumentException()
        {
            ICoordinate coordinate = new Coordinate3D(0, 0, 0);

            Assert.Throws<ArgumentException>(() => coordinate.Equals(5));
        }

        [TestCase(6, 6, 6, 6, 6, 6)]
        [TestCase(0, 0, 0, 0, 0, 0)]
        [TestCase(10, 10, 10, 10, 10, 10)]
        public void Equals_ComparesEqualCoordinate3D_ReturnsTrue(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            ICoordinate first = new Coordinate3D(x1, y1, z1);
            ICoordinate second = new Coordinate3D(x2, y2, z2);

            var areEqual = first.Equals(second);

            Assert.IsTrue(areEqual);
        }

        [TestCase(6, 6, 6, 7, 6, 6)]
        [TestCase(0, 0, 0, 1, 0, 0)]
        [TestCase(10, 10, 10, 12, 10, 10)]
        public void Equals_ComparesNotEqualCoordinates3D_ReturnsFalse(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            ICoordinate first = new Coordinate3D(x1, y1, z1);
            ICoordinate second = new Coordinate3D(x2, y2, z2);

            var areEqual = first.Equals(second);

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Equals_Compares3DAnd2DCoordinates_ReturnsFalse()
        {
            ICoordinate coordinate2D = new Coordinate2D(3, 3);
            ICoordinate coordinate3D = new Coordinate3D(3, 3, 3);

            var areEqual = coordinate3D.Equals(coordinate2D);

            Assert.IsFalse(areEqual);
        }

        [Test]
        public void Clone_Coordinate3D_ReturnsANewInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate3D(5, 5, 5);

            var clone = coordinate.Clone();

            Assert.IsInstanceOf(typeof(Coordinate3D), clone);
            Assert.IsTrue(clone.Equals(coordinate));
            Assert.AreNotSame(clone, coordinate);
        }
    }
}