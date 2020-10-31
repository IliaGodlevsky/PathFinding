using System.Linq;
using System.Threading;
using Algorithm.Extensions;
using Algorithm.PathFindingAlgorithms;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.UnitTests.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLib.UnitTests.Tests.PathFindingAlgorithmsTests
{
    [TestClass]
    public class LeeAlgorithmTests
    {
        [TestMethod]
        public void LeeAlgorithmFindPath_NullGraph_ReturnsEmptyPath()
        {
            var leeAlgorithm = new LeeAlgorithm(NullGraph.Instance);

            leeAlgorithm.FindPath();

            Assert.IsFalse(leeAlgorithm.GetPath().Any());
        }

        [TestMethod]
        public void LeeAlgorithmFindPath_NotNullGraph_ReturnsNotEmptyPath()
        {
            var startVertexPosition = new Coordinate2D(1, 1);
            var endVertexPosition = new Coordinate2D(9, 9);
            var parametres = new GraphParametres(width: 10, height: 10, obstaclePercent: 0);
            var factory = new Graph2dFactory(parametres);
            var graph = factory.CreateGraph(() => new TestVertex());
            Thread.Sleep(millisecondsTimeout: 230);
            graph.Start = graph[startVertexPosition];
            graph.End = graph[endVertexPosition];
            var leeAlgorithm = new LeeAlgorithm(graph);

            leeAlgorithm.FindPath();

            Assert.IsTrue(leeAlgorithm.GetPath().Any());
        }
    }
}
