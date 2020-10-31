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
    public class DijkstraAlgorithmTests
    {
        [TestMethod]
        public void FindPath_NullGraph_ReturnsEmptyPath()
        {
            var dikstraAlgorithm = new DijkstraAlgorithm(NullGraph.Instance);

            dikstraAlgorithm.FindPath();

            Assert.IsFalse(dikstraAlgorithm.GetPath().Any());
        }

        [TestMethod]
        public void FindPath_NotNullGraph_ReturnsNotEmptyPath()
        {
            
            var startVertexPosition = new Coordinate2D(x: 1, y: 1);
            var endVertexPosition = new Coordinate2D(x: 9, y: 9);
            var parametres = new GraphParametres(width: 10, height: 10, obstaclePercent: 0);
            var factory = new Graph2dFactory(parametres);
            var graph = factory.CreateGraph(() => new TestVertex());
            Thread.Sleep(millisecondsTimeout: 230);
            graph.Start = graph[startVertexPosition];
            graph.End = graph[endVertexPosition];
            var dikstraAlgorithm = new DijkstraAlgorithm(graph);

            dikstraAlgorithm.FindPath();

            Assert.IsTrue(dikstraAlgorithm.GetPath().Any());
        }
    }
}
