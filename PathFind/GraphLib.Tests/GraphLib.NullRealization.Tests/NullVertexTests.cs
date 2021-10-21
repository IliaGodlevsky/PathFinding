using GraphLib.Extensions;
using GraphLib.NullRealizations.NullObjects;
using NUnit.Framework;
using System;
using System.Linq;

namespace GraphLib.Common.Tests
{
    [TestFixture]
    public class NullVertexTests
    {
        [Test]
        public void IsIsolated_ReturnTrue()
        {
            var vertex = new NullVertex();

            Assert.IsTrue(vertex.IsIsolated());
        }

        [Test]
        public void SetToDefault_DoesntThrow()
        {
            var vertex = new NullVertex();

            Assert.DoesNotThrow(() => vertex.SetToDefault());
        }

        [Test]
        public void Initialize_DoesntThrow()
        {
            var vertex = new NullVertex();

            Assert.DoesNotThrow(() => vertex.Initialize());
        }

        [Test]
        public void IsEqual_NullVertex_ReturnTrue()
        {
            var vertex = new NullVertex();
            var secondVertex = new NullVertex();

            Assert.IsTrue(vertex.IsEqual(secondVertex));
        }

        [Test]
        public void SetNeighbours_DoesnotThrows()
        {
            var graph = new NullGraph();
            var vertex = graph.FirstOrNullVertex();

            Assert.DoesNotThrow(() => vertex.SetNeighbours(graph));
        }

        [Test]
        public void IsNeighbour_DoesnotThrow()
        {
            NullGraph graph = new NullGraph();

            var candidate = new NullVertex();
            var vertex = graph.FirstOrNullVertex();

            Assert.DoesNotThrow(() => vertex.IsNeighbour(candidate));
        }
    }
}
