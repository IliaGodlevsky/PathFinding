using GraphLibrary.Graphs.Interface;
using GraphLibrary.UnitTests.Classes;
using GraphLibrary.Vertex.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.GraphTests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void GetIndices_VertexBelongsToGraph_ReturnsRightIndices()
        {
            var factory = new GraphFactory.GraphFactory(new GraphParametres(25, 25, 0), 25);
            var graph = factory.GetGraph(() => new TestVertex());

            var vertex = graph[15, 15];

            Assert.AreEqual(vertex.Position, graph.GetIndices(vertex));
        }

        [TestMethod]
        public void GetIndices_VertexDoesNotBelongToGraph_ReturnsWrongIndices()
        {
            var factory = new GraphFactory.GraphFactory(new GraphParametres(25, 25, 0), 25);
            var graph = factory.GetGraph(() => new TestVertex());

            var vertex = new TestVertex
            {
                Position = new Position(0, 0)
            };

            Assert.AreNotEqual(vertex.Position, graph.GetIndices(vertex));
        }
    }
}
