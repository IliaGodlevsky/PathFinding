using System;
using System.Linq;
using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLib.Test.CoordinateTests
{
    [TestClass]
    public class Coordinate3DTests
    {
        private const int NeighboursOfCoordinate3D = 26;

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Constructor_FourCoordinateValues_ThrowsArgumentOutOfRangeException()
        {
            _ = new Coordinate3D(1, 2, 3, 4);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Constructor_OneCoordinateValue_ThrowsArgumentOutOfRangeException()
        {
            _ = new Coordinate3D(1);
        }

        [TestMethod]
        public void Constructor_ThreeCoordinateValues_ReturnsInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate3D(2, 2, 2);

            Assert.IsInstanceOfType(coordinate, typeof(Coordinate3D));
        }

        [TestMethod]
        public void Environment_Coordinate2DX5_Y5_Z5_ReturnEightCoordinates()
        {
            ICoordinate coordinate = new Coordinate3D(5, 5, 5);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Count() == NeighboursOfCoordinate3D);
        }

        [TestMethod]
        public void Environment_CoordinateValues5And5And5_ReturnUniqueCoordinates()
        {
            ICoordinate coordinate = new Coordinate3D(5, 5, 5);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Distinct().Count() == NeighboursOfCoordinate3D);
        }

        [TestMethod]
        public void Environment_Coordinate2DX5Y5Z5_ReturnEightCoordinatesWithoutX5Y5()
        {
            ICoordinate coordinate = new Coordinate3D(5, 5, 5);

            var environment = coordinate.Environment;

            Assert.IsFalse(environment.Contains(coordinate));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Equals_ComparesIntAndCoordinate3D_ThrowsArgumentException()
        {
            ICoordinate coordinate = new Coordinate3D(0, 0, 0);

            coordinate.Equals(5);
        }

        [TestMethod]
        public void Equals_ComparesEqualCoordinate3D_ReturnsTrue()
        {
            ICoordinate first = new Coordinate3D(6, 6, 6);
            ICoordinate second = new Coordinate3D(6, 6, 6);

            var areEqual = first.Equals(second);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_ComparesNotEqualCoordinate3D_ReturnsFalse()
        {
            ICoordinate first = new Coordinate3D(5, 5, 5);
            ICoordinate second = new Coordinate3D(6, 6, 6);

            var areEqual = first.Equals(second);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_Compares3DAnd2DCoordinates_ReturnsFalse()
        {
            ICoordinate coordinate2D = new Coordinate2D(3, 3);
            ICoordinate coordinate3D = new Coordinate3D(3, 3, 3);

            var areEqual = coordinate3D.Equals(coordinate2D);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Clone_Coordinate3D_ReturnsANewInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate3D(5, 5, 5);

            var clone = coordinate.Clone();

            Assert.IsInstanceOfType(clone, typeof(Coordinate3D));
            Assert.IsTrue(clone.Equals(coordinate));
            Assert.AreNotSame(clone, coordinate);
        }
    }
}