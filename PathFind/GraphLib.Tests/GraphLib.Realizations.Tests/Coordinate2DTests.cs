﻿using GraphLib.Exceptions;
using GraphLib.Realizations.Coordinates;
using NUnit.Framework;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class Coordinate2DTests
    {
        [TestCase(1)]
        [TestCase(1, 2, 3)]
        [TestCase]
        public void Constructor_InvalidNumberOfCoordinateValues_ThrowsWrongNumberOfDimensionsException(params int[] coordinates)
        {
            Assert.Throws<WrongNumberOfDimensionsException>(() => new Coordinate2D(coordinates));
        }
    }
}