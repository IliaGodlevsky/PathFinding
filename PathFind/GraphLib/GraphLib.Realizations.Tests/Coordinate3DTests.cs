using NUnit.Framework;
using System;

namespace GraphLib.Realizations.Tests
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
    }
}