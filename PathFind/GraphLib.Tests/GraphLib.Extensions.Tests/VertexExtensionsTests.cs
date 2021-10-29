using GraphLib.Interfaces;
using GraphLib.NullRealizations.NullObjects;
using GraphLib.Realizations.Coordinates;
using GraphLib.TestRealizations.TestFactories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            vertex.Neighbours = new List<IVertex>();

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
            var vertex = graph.FirstOrNullVertex();

            Assert.Throws<ArgumentException>(() => vertex.SetNeighbours(otherGraph));
        }

        [Test]
        public void SetNeighbours_NullVertex_NullGraph_NeighboursArrayAreEmpty()
        {
            var graph = new NullGraph();
            var vertex = graph.FirstOrNullVertex();

            vertex.SetNeighbours(graph);

            Assert.IsFalse(vertex.Neighbours.Any());
        }
    }
}
