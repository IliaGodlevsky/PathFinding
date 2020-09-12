using GraphLibrary.GraphFactory;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.UnitTests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests
{
    [TestClass]
    public class GraphFactoryTest
    {
        [TestMethod]
        public void GetGraph_NotZeroParams_ReturnsGraphWithSameParams()
        {
            int width = 25;
            int height = 25;
            int obstaclePercent = 25;
            var factory = new GraphFactory(new GraphParametres(width,
                height, obstaclePercent), placeBetweenVertices: 0);
            var graph = factory.GetGraph(() => new TestVertex());

            Assert.AreEqual(graph.Width, width);
            Assert.AreEqual(graph.Height, height);
            Assert.AreEqual(graph.Size, width * height);
        }
    }
}
