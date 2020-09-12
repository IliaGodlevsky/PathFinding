using GraphLibrary.Graphs.Interface;
using GraphLibrary.Vertex.Interface;
using GraphLibraryTest.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void GetIndicesTest_VertexBelongsToGraph_ReturnsRightIndices()
        {
            var factory = new GraphFactory.GraphFactory(new GraphParametres(25, 25, 0), 25);
            var graph = factory.GetGraph(() => new TestVertex());

            var vertex = graph[15, 15];

            Assert.AreEqual(vertex.Position, graph.GetIndices(vertex));
        }

        [TestMethod]
        public void GetIndicesTest_VertexDoesNotBelongToGraph_ReturnsWrongIndices()
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
