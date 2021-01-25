using GraphLib.Coordinates;
using GraphLib.Coordinates.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace GraphLib.Test
{
    [TestClass]
    public class Coordinate2DTests
    {
        private const int NeighboursOfCoordinate2D = 8;

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Constructor_3CoordinateValues_ThrowsArgumentOutOfRangeException()
        {
            _ = new Coordinate2D(1, 2, 3);
        }

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void Constructor_1CoordinateValues_ThrowsArgumentOutOfRangeException()
        {
            _ = new Coordinate2D(1);
        }

        [TestMethod]
        public void Constructor_2CoordinateValues_ReturnsInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate2D(2, 2);

            Assert.IsInstanceOfType(coordinate, typeof(Coordinate2D));
        }

        [TestMethod]
        public void Environment_Coordinate2DX5_Y5_ReturnEightCoordinates()
        {
            ICoordinate coordinate = new Coordinate2D(5, 5);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Count() == NeighboursOfCoordinate2D);
        }

        [TestMethod]
        public void Environment_CoordinateValues5And5_ReturnUniqueCoordinates()
        {
            ICoordinate coordinate = new Coordinate2D(5, 5);

            var environment = coordinate.Environment;

            Assert.IsTrue(environment.Distinct().Count() == NeighboursOfCoordinate2D);
        }

        [TestMethod]
        public void Environment_Coordinate2DX5Y5_ReturnEightCoordinatesWithoutX5Y5()
        {
            ICoordinate coordinate = new Coordinate2D(5, 5);

            var environment = coordinate.Environment;

            Assert.IsFalse(environment.Contains(coordinate));
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void Equals_ComparesIntAndCoordinate2D_ThrowsArgumentException()
        {
            ICoordinate coordinate = new Coordinate2D(0, 0);

            coordinate.Equals(5);
        }

        [TestMethod]
        public void Equals_ComparesEqualCoordinate2D_ReturnsTrue()
        {
            ICoordinate first = new Coordinate2D(6, 6);
            ICoordinate second = new Coordinate2D(6, 6);

            var areEqual = first.Equals(second);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_ComparesNotEqualCoordinate2D_ReturnsFalse()
        {
            ICoordinate first = new Coordinate2D(5, 5);
            ICoordinate second = new Coordinate2D(6, 6);

            var areEqual = first.Equals(second);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_Compares2DAnd3DCoordinates_ReturnsFalse()
        {
            ICoordinate coordinate2D = new Coordinate2D(3, 3);
            ICoordinate coordinate3D = new Coordinate3D(3, 3, 3);

            var areEqual = coordinate2D.Equals(coordinate3D);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Clone_Coordinate2D_ReturnsANewInstanceOfCoordinate2DClass()
        {
            ICoordinate coordinate = new Coordinate2D(5, 5);

            var clone = coordinate.Clone();

            Assert.IsInstanceOfType(clone, typeof(Coordinate2D));
            Assert.IsTrue(clone.Equals(coordinate));
            Assert.AreNotSame(clone, coordinate);
        }
    }
}
