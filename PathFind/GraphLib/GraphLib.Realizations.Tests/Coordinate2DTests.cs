using NUnit.Framework;
using System;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class Coordinate2DTests
    {
        [TestCase(1)]
        [TestCase(1, 2, 3)]
        [TestCase()]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsArgumentOutOfRangeException(params int[] coordinates)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Coordinate2D(coordinates));
        }
    }
}