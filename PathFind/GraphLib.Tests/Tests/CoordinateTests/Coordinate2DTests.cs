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