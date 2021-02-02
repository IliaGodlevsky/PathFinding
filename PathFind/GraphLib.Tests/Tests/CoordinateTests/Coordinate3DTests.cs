using GraphLib.Interface;
using GraphLib.NullObjects;
using NUnit.Framework;
using System;

namespace GraphLib.Tests.Tests.CoordinateTests
{
    [TestFixture]
    public class Coordinate3DTests
    {
        [TestCase(1)]
        [TestCase(1, 2)]
        [TestCase()]
        [TestCase(1, 2, 3, 4)]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsArgumentOutOfRangeException(params int[] coordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate3D(coordinates));
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