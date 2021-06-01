using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Coordinates;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Extensions.Tests
{
    [TestFixture]
    public class VertexExtensionsTests
    {
        private readonly TestGraph2DAssemble graphAssembler;

        public VertexExtensionsTests()
        {
            graphAssembler = new TestGraph2DAssemble();
        }

        [Test]
        public void SetNeighbours_ReturnsRightNumberOfNeighbours()
        {
            const int expectedNumberOfNeighbours = 8;
            var graph = graphAssembler.AssembleGraph();
            var vertex = graph[new Coordinate2D(5, 7)];
            vertex.Neighbours.Clear();

            vertex.SetNeighbours(graph);

            Assert.AreEqual(expectedNumberOfNeighbours, vertex.Neighbours.Count);
        }

        [Test]
        public void SetNeighbours_VertexFromOtherGraph_ThrowsArgumentException()
        {
            var graph = graphAssembler.AssembleGraph();
            var otherGraph = graphAssembler.AssembleGraph();
            var vertex = graph.Vertices.ElementAt(45);

            Assert.Throws<ArgumentException>(() => vertex.SetNeighbours(otherGraph));
        }

        [Test]
        public void SetNeighbours_NullVertexFromOtherNullGraph_ThrowsArgumentException()
        {
            var graph = new NullGraph();
            var otherGraph = new NullGraph();
            var vertex = graph.Vertices.First();

            Assert.Throws<ArgumentException>(() => vertex.SetNeighbours(otherGraph));
        }

        [Test]
        public void SetNeighbours_NullVertex_ReturnsRightNumberOfNeighbours()
        {
            const int expectedNumberOfNeighbours = 1;
            var graph = graphAssembler.AssembleGraph();
            graph[new TestCoordinate(0, 0)] = new NullVertex();

            NullVertex vertex = (NullVertex)graph.Vertices.First();

            vertex.SetNeighbours(graph);

            Assert.AreEqual(expectedNumberOfNeighbours, vertex.Neighbours.Count);
        }

        [Test]
        public void SetNeighbours_NullVertex_NullGraph_ReturnsRightNumberOfNeighbours()
        {
            const int expectedNumberOfNeighbours = 1;
            var graph = new NullGraph();
            NullVertex vertex = (NullVertex)graph.Vertices.First();

            vertex.SetNeighbours(graph);

            Assert.AreEqual(expectedNumberOfNeighbours, vertex.Neighbours.Count);
        }
    }
}
