using GraphLib.Extensions;
using GraphLib.NullRealizations;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Common.Tests
{
    [TestFixture]
    public class NullVertexTests
    {
        [Test]
        public void IsIsolated_ReturnTrue()
        {
            var vertex = NullVertex.Instance;

            Assert.IsTrue(vertex.IsIsolated());
        }

        [Test]
        public void SetToDefault_DoesntThrow()
        {
            var vertex = NullVertex.Instance;

            Assert.DoesNotThrow(() => vertex.SetToDefault());
        }

        [Test]
        public void Initialize_DoesntThrow()
        {
            var vertex = NullVertex.Instance;

            Assert.DoesNotThrow(() => vertex.Initialize());
        }

        [Test]
        public void IsEqual_NullVertex_ReturnTrue()
        {
            var vertex = NullVertex.Instance;
            var secondVertex = NullVertex.Instance;

            Assert.IsTrue(vertex.IsEqual(secondVertex));
        }

        [Test]
        public void IsNeighbour_DoesnotThrow()
        {
            var graph = NullGraph.Instance;

            var candidate = NullVertex.Instance;
            var vertex = graph.Vertices.FirstOrDefault() ?? NullVertex.Instance;

            Assert.DoesNotThrow(() => vertex.IsNeighbour(candidate));
        }
    }
}
