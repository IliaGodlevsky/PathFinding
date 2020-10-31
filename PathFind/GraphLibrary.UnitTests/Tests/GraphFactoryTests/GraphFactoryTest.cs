using Common.ValueRanges;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.UnitTests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLib.UnitTests.Tests.GraphFactoryTests
{
    [TestClass]
    public class GraphFactoryTest
    {
        [TestMethod]
        public void GetGraph_NotZeroParams_ReturnsGraphWithSameParams()
        {
            var parametres = new GraphParametres(width: 25, height: 25, obstaclePercent: 25);
            var factory = new Graph2dFactory(parametres);

            var graph = factory.CreateGraph(() => new TestVertex());

            Assert.AreEqual(graph.Size, parametres.Width * parametres.Height);
            Assert.IsTrue(Range.ObstaclePercentValueRange.IsInBounds(graph.ObstaclePercent));
        }
    }
}
