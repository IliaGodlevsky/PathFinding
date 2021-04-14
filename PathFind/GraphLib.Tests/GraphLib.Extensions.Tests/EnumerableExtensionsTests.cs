using System;
using GraphLib.Interface;
using Moq;
using NUnit.Framework;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    internal class EnumerableExtensionsTests
    {
        private Mock<IGraph> graphMock;

        [SetUp]
        public void SetUp()
        {
            graphMock = new Mock<IGraph>();
        }

        [TestCase(new[] { 8, 16 }, new[] { 10, 20 }, ExpectedResult = true)]
        [TestCase(new[] { 33, 1, 100 }, new[] { 60, 50, 150 }, ExpectedResult = true)]
        [TestCase(new[] { 33, 1, 100, 50 }, new[] { 60, 50, 150, 67 }, ExpectedResult = true)]
        [TestCase(new[] { 11, 16 }, new[] { 10, 20 }, ExpectedResult = false)]
        [TestCase(new[] { 33, 52, 100 }, new[] { 60, 50, 150 }, ExpectedResult = false)]
        [TestCase(new[] { 33, 1, 100, 69 }, new[] { 60, 50, 150, 67 }, ExpectedResult = false)]
        public bool IsWithinGraph_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidCondition(
            int[] coordinateValues,
            int[] graphDimensionSizes)
        {
            graphMock.Setup(graph => graph.DimensionsSizes).Returns(graphDimensionSizes);

            return coordinateValues.IsWithinGraph(graphMock.Object);
        }

        [TestCase(new [] { 33, 52, 100 })]
        [TestCase(new [] { 33, 1, 100, 69 })]
        public void IsWithinGraph_GraphIsNull_ThrowsArgumentNullException(int[] coordinateValues)
        {
            IGraph graph = null;

            Assert.Throws<ArgumentNullException>(() => coordinateValues.IsWithinGraph(graph));
        }

        [TestCase(new[] { 1 }, new [] { 5 }, ExpectedResult = 1)]
        [TestCase(new[] { 6 }, new[] { 7 }, ExpectedResult = 6)]
        [TestCase(new[] { 25 }, new[] { 35 }, ExpectedResult = 25)]
        [TestCase(new[] { 2 }, new[] { 3 }, ExpectedResult = 2)]
        [TestCase(new[] { 1, 2 }, new[] { 67, 32 }, ExpectedResult = 34)]
        [TestCase(new[] { 1, 5 }, new[] { 70, 20 }, ExpectedResult = 25)]
        [TestCase(new[] { 35, 14 }, new[] { 40, 20 }, ExpectedResult = 714)]
        [TestCase(new[] { 45, 2 }, new[] { 47, 20 }, ExpectedResult = 902)]
        [TestCase(new[] { 10, 20, 30 }, new[] { 15, 25, 45 }, ExpectedResult = 12180)]
        [TestCase(new[] { 14, 12, 34 }, new[] { 16, 13, 39 }, ExpectedResult = 7600)]
        [TestCase(new[] { 54, 22, 99 }, new[] { 60, 25, 103 }, ExpectedResult = 141415)]
        [TestCase(new[] { 61, 31, 3 }, new[] { 62, 32, 10 }, ExpectedResult = 19833)]
        [TestCase(new[] { 10, 20, 30, 40 }, new[] { 15, 25, 45, 50 }, ExpectedResult = 609040)]
        [TestCase(new[] { 14, 12, 34, 1 }, new[] { 16, 13, 39, 3 }, ExpectedResult = 22801)]
        [TestCase(new[] { 54, 22, 99, 37 }, new[] { 60, 25, 103, 45 }, ExpectedResult = 6363712)]
        [TestCase(new[] { 61, 31, 3, 14 }, new[] { 62, 32, 10, 20 }, ExpectedResult = 396674)]
        public int ToIndex_CoordinateIsOfSameDimensionAsTheGraph_ReturnsValidIndex(
            int[] coordinateValues, int[] dimensionSizes)
        {
            return coordinateValues.ToIndex(dimensionSizes);
        }

        [TestCase(new[] { 67, 32 }, new[] { 67, 32 })]
        [TestCase(new[] { 70, 35, 14 }, new[] { 67, 32, 10 })]
        [TestCase(new[] { 70, 25 }, new[] { 67, 32 })]
        public void ToIndex_CoordinateIsOutOfGraph_ThrowsException(
            int[] coordinateValues, int[] graphDimensionSizes)
        {
            Assert.Throws<Exception>(() => coordinateValues.ToIndex(graphDimensionSizes));
        }

        [TestCase(new[] { 25, 11 }, new[] { 4, 7, 8 })]
        [TestCase(new[] { 0 }, new[] { 16, 17 })]
        [TestCase(new[] { 1, 4, 1, 7 }, new[] { 16, 17, 18 })]
        public void ToIndex_CoordinateIsNotOfSameDimensionAsTheGraph_ThrowsArgumentOutOfRangeException(
            int[] coordinateValues, int[] graphDimensionSizes)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => coordinateValues.ToIndex(graphDimensionSizes));
        }
    }
}
