using Common.Extensions;
using GraphLib.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class GraphExtensionsTests
    {
        private Mock<IGraph> graphMock;

        [SetUp]
        public void Setup()
        {
            graphMock = new Mock<IGraph>();
        }

        [TestCase(new[] { 11, 12 })]
        [TestCase(new[] { 11, 12, 13 })]
        public void ToCoordinates_IndexIsOutOfRange_ThrowsArgumentOurOfRangeException(int[] dimensionSizes)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => dimensionSizes.ToCoordinates(-1));
        }

        [TestCase(new[] { 11, 12 })]
        public void ToCoordinates_IndexIsInRange_ReturnCoordinates(int[] dimensionSizes)
        {
            var expectedCoordinates = new[] { 7, 3 };
            int index = 40;

            var coordinates = dimensionSizes.ToCoordinates(index);

            Assert.IsTrue(coordinates.SequenceEqual(expectedCoordinates));
        }
    }
}
