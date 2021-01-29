using GraphLib.Coordinates.Abstractions;
using GraphLib.Extensions;
using GraphLib.Graphs.Abstractions;
using Moq;
using NUnit.Framework;

namespace GraphLib.Tests.Tests.CoordinateTests.ExtensionsTests
{
    [TestFixture]
    class IsWithinGraphTests
    {
        private Mock<IGraph> graphMock;
        private Mock<ICoordinate> coordinateMock;

        [SetUp]
        public void SetUp()
        {
            graphMock = new Mock<IGraph>();
            coordinateMock = new Mock<ICoordinate>();
        }

        [TestCase(new int[] { 8, 16 },          new int[] { 10, 20 },           ExpectedResult = true)]
        [TestCase(new int[] { 33, 1, 100 },     new int[] { 60, 50, 150 },      ExpectedResult = true)]
        [TestCase(new int[] { 33, 1, 100, 50 }, new int[] { 60, 50, 150, 67 },  ExpectedResult = true)]
        [TestCase(new int[] { 11, 16 },         new int[] { 10, 20 },           ExpectedResult = false)]
        [TestCase(new int[] { 33, 52, 100 },    new int[] { 60, 50, 150 },      ExpectedResult = false)]
        [TestCase(new int[] { 33, 1, 100, 69 }, new int[] { 60, 50, 150, 67 },  ExpectedResult = false)]
        public bool IsWithinGraph_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidCondition(
            int[] coordinateValues,
            int[] graphDimensionSizes)
        {
            coordinateMock.Setup(coordinate => coordinate.CoordinatesValues).Returns(coordinateValues);
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(graphDimensionSizes);
            var coordinate = coordinateMock.Object;
            var graph = graphMock.Object;

            return coordinate.IsWithinGraph(graph);

        }
    }
}
