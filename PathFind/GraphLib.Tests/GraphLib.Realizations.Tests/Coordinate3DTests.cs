using GraphLib.Exceptions;
using GraphLib.Realizations.Coordinates;
using NUnit.Framework;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class Coordinate3DTests
    {
        [TestCase(1)]
        [TestCase(1, 2)]
        [TestCase()]
        [TestCase(1, 2, 3, 4)]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsWrongNumberOfDimensionsException(params int[] coordinates)
        {
            Assert.Throws<WrongNumberOfDimensionsException>(() => new Coordinate3D(coordinates));
        }
    }
}