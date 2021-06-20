using GraphLib.Extensions;
using GraphLib.NullRealizations.NullObjects;
using NUnit.Framework;
using System.Linq;

namespace GraphLib.Common.Tests
{
    [TestFixture]
    public class NullGraphTests
    {
        [Test]
        public void ToCoordinates_NullGraph_IndexIsInRange_ReturnEmptyCoordinates()
        {
            var expectedCoordinates = new int[] { };
            NullGraph graph = new NullGraph();
            int index = 0;

            var coordinates = graph.ToCoordinates(index);

            Assert.IsTrue(coordinates.SequenceEqual(expectedCoordinates));
        }

        [Test]
        public void Refresh_DoesnotThrows()
        {
            var graph = new NullGraph();

            Assert.DoesNotThrow(() => graph.Refresh());
        }

        [Test]
        public void ToWeighted_DoesntThrow()
        {
            var graph = new NullGraph();

            Assert.DoesNotThrow(() => graph.ToWeighted());
        }

        [Test]
        public void ToUnweighted_DoesntThrow()
        {
            var graph = new NullGraph();

            Assert.DoesNotThrow(() => graph.ToUnweighted());
        }

        [Test]
        public void ForEach_DoesntThrow()
        {
            var graph = new NullGraph();

            Assert.DoesNotThrow(() => graph.ForEach(vertex => vertex.GetHashCode()));
        }

        [Test]
        public void ConnectVertices_DoesntThrow()
        {
            var graph = new NullGraph();

            Assert.DoesNotThrow(() => graph.ConnectVertices());
        }
    }
}