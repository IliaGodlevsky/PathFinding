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
            graphMock.Setup(g => g.DimensionsSizes).Returns(dimensionSizes);
            graphMock.Setup(g => g.Size).Returns(dimensionSizes.AggregateOrDefault(IntExtensions.Multiply));
            var graph = graphMock.Object;
            int index = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => graph.ToCoordinates(index));
        }

        [TestCase(new[] { 11, 12 })]
        public void ToCoordinates_IndexIsInRange_ReturnCoordinates(int[] dimensionSizes)
        {
            var expectedCoordinates = new[] { 7, 3 };
            graphMock.Setup(g => g.DimensionsSizes).Returns(dimensionSizes);
            graphMock.Setup(g => g.Size).Returns(dimensionSizes.AggregateOrDefault(IntExtensions.Multiply));
            var graph = graphMock.Object;
            int index = 40;

            var coordinates = graph.ToCoordinates(index);

            Assert.IsTrue(coordinates.SequenceEqual(expectedCoordinates));
        }
    }
}
