using GraphLib.Interfaces;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class CoordinateExtensionsTests
    {
        [TestCase(new[] { 6, 6 }, new[] { 6, 6 }, ExpectedResult = true)]
        [TestCase(new[] { 0, 0, 0 }, new[] { 0, 0, 0 }, ExpectedResult = true)]
        [TestCase(new[] { 10, 10, 10, 10 }, new[] { 10, 10, 10, 10 }, ExpectedResult = true)]
        [TestCase(new[] { 6, 7 }, new[] { 5, 6 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 0 }, new[] { 1, 0, 2 }, ExpectedResult = false)]
        [TestCase(new[] { 10, 9, 10, 10 }, new[] { 8, 10, 10, 10 }, ExpectedResult = false)]
        [TestCase(new[] { 0, 1, 0 }, new[] { 1, 0 }, ExpectedResult = false)]
        [TestCase(new[] { 10, 9 }, new[] { 8, 10, 10, 10 }, ExpectedResult = false)]
        public bool IsEqual_ComparesCoordinatesWithVariousCoordinateValues_ReturnsValidCondition(
            int[] firstCoordinateValues, int[] secondCoordinateValues)
        {
            TestCoordinate first = new TestCoordinate(firstCoordinateValues);
            TestCoordinate second = new TestCoordinate(secondCoordinateValues);

            return first.IsEqual(second);
        }

        [Test]
        public void IsEqual_CoordinatesIsNull_ReturnsFalse()
        {
            ICoordinate first = null;
            ICoordinate second = null;

            bool isEqual = first.IsEqual(second);

            Assert.IsFalse(isEqual);
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
            TestGraph graph = new TestGraph(graphDimensionSizes);
            TestCoordinate coordinate = new TestCoordinate(coordinateValues);

            return coordinate.IsWithinGraph(graph);
        }

        [TestCase(new[] { 33, 52, 100 })]
        [TestCase(new[] { 33, 1, 100, 69 })]
        public void IsWithinGraph_GraphIsNull_ThrowsArgumentNullException(int[] coordinateValues)
        {
            IGraph graph = null;
            TestCoordinate coordinate = new TestCoordinate(coordinateValues);

            Assert.Throws<ArgumentNullException>(() => coordinate.IsWithinGraph(graph));
        }
    }
}
