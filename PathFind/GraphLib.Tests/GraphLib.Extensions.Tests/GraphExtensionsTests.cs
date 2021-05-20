using GraphLib.Extensions.Tests.TestObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class GraphExtensionsTests
    {
        [TestCase(new[] { 11, 12 })]
        [TestCase(new[] { 11, 12, 13 })]
        public void ToCoordinates_IndexIsOutOfRange_ThrowsArgumentOurOfRangeException(int[] dimensionSizes)
        {
            var graph = new TestGraph(dimensionSizes);
            int index = graph.Size;

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.ToCoordinates(index));
        }

        [TestCase(new[] { 11, 12 })]
        public void ToCoordinates_IndexIsInRange_ReturnCoordinates(int[] dimensionSizes)
        {
            var expectedCoordinates = new[] { 7, 3 };
            var graph = new TestGraph(dimensionSizes);
            int index = 40;

            var coordinates = graph.ToCoordinates(index);

            Assert.IsTrue(coordinates.SequenceEqual(expectedCoordinates));
        }
    }
}
