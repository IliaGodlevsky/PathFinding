using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Moq;
using NUnit.Framework;
using System;

namespace GraphLib.Tests.Tests.CoordinateTests.ExtensionsTests
{
    [TestFixture]
    public class ToIndexTests
    {
        private Mock<IGraph> graphMock;
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            graphMock = new Mock<IGraph>();
            coordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(new int[] { 1 },              new int[] { 5 },                ExpectedResult = 1)]
        [TestCase(new int[] { 6 },              new int[] { 7 },                ExpectedResult = 6)]
        [TestCase(new int[] { 25 },             new int[] { 35 },               ExpectedResult = 25)]
        [TestCase(new int[] { 2 },              new int[] { 3 },                ExpectedResult = 2)]
        [TestCase(new int[] { 1, 2 },           new int[] { 67, 32 },           ExpectedResult = 34)]
        [TestCase(new int[] { 1, 5 },           new int[] { 70, 20 },           ExpectedResult = 25)]
        [TestCase(new int[] { 35, 14 },         new int[] { 40, 20 },           ExpectedResult = 714)]
        [TestCase(new int[] { 45, 2 },          new int[] { 47, 20 },           ExpectedResult = 902)]
        [TestCase(new int[] { 10, 20, 30 },     new int[] { 15, 25, 45 },       ExpectedResult = 12180)]
        [TestCase(new int[] { 14, 12, 34 },     new int[] { 16, 13, 39 },       ExpectedResult = 7600)]
        [TestCase(new int[] { 54, 22, 99 },     new int[] { 60, 25, 103 },      ExpectedResult = 141415)]
        [TestCase(new int[] { 61, 31, 3 },      new int[] { 62, 32, 10 },       ExpectedResult = 19833)]
        [TestCase(new int[] { 10, 20, 30, 40 }, new int[] { 15, 25, 45, 50 },   ExpectedResult = 609040)]
        [TestCase(new int[] { 14, 12, 34, 1 },  new int[] { 16, 13, 39, 3 },    ExpectedResult = 22801)]
        [TestCase(new int[] { 54, 22, 99, 37 }, new int[] { 60, 25, 103, 45 },  ExpectedResult = 6363712)]
        [TestCase(new int[] { 61, 31, 3, 14 },  new int[] { 62, 32, 10, 20 },   ExpectedResult = 396674)]
        public int ToIndex_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidIndex(
            int[] coordinateValues, int[] dimensionSizes)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(dimensionSizes);
            var coordinate = coordinateMock.Object;
            var graph = graphMock.Object;

            return coordinate.ToIndex(graph);
        }

        [TestCase(new int[] { 67, 32 },     new int[] { 67, 32 })]
        [TestCase(new int[] { 70, 35, 14 }, new int[] { 67, 32, 10 })]
        [TestCase(new int[] { 70, 25 },     new int[] { 67, 32 })]
        public void ToIndex_CoordinateIsOutOfGraph_ThrowsArgumentOutOfRangeException(
            int[] coordinateValues, int[] graphDimensionSizes)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(graphDimensionSizes);
            var coordinate = coordinateMock.Object;
            var graph = graphMock.Object;

            Assert.Throws<ArgumentOutOfRangeException>(() => coordinate.ToIndex(graphMock.Object));
        }

        [TestCase(new int[] { 25, 11 },     new int[] { 4, 7, 8 })]
        [TestCase(new int[] { 0 },          new int[] { 16, 17 })]
        [TestCase(new int[] { 1, 4, 1, 7 }, new int[] { 16, 17, 18 })]
        public void ToIndex_CoordinateIsNotOfSameDimensionAsTheGraph_ThrowsArgumentException(
            int[] coordinateValues, int[] graphDimensionSizes)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(graphDimensionSizes);
            var coordinate = coordinateMock.Object;
            var graph = graphMock.Object;

            Assert.Throws<ArgumentException>(() => coordinate.ToIndex(graph));
        }
    }
}
