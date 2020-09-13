using GraphLibrary.Graphs.Interface;
using GraphLibrary.UnitTests.Classes;
using GraphLibrary.ValueRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.GraphFactoryTests
{
    [TestClass]
    public class GraphFactoryTest
    {
        [TestMethod]
        public void GetGraph_NotZeroParams_ReturnsGraphWithSameParams()
        {
            var parametres = new GraphParametres(width: 25, height: 25, obstaclePercent: 25);
            var factory = new GraphFactory.GraphFactory(parametres, placeBetweenVertices: 0);

            var graph = factory.GetGraph(() => new TestVertex());

            Assert.AreEqual(graph.Width, parametres.Width);
            Assert.AreEqual(graph.Height, parametres.Height);
            Assert.AreEqual(graph.Size, parametres.Width * parametres.Height);
            Assert.IsTrue(Range.ObstaclePercentValueRange.IsInBounds(graph.ObstaclePercent));
        }
    }
}
