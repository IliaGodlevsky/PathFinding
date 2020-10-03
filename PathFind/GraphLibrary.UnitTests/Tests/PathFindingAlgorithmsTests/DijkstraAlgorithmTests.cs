using System.Linq;
using System.Threading;
using GraphLibrary.Coordinates;
using GraphLibrary.Extensions.CustomTypeExtensions;
using GraphLibrary.GraphCreating;
using GraphLibrary.Graphs;
using GraphLibrary.Graphs.Interface;
using GraphLibrary.PathFindingAlgorithm;
using GraphLibrary.UnitTests.Classes;
using GraphLibrary.Vertex.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphLibrary.UnitTests.Tests.PathFindingAlgorithmsTests
{
    [TestClass]
    public class DijkstraAlgorithmTests
    {
        [TestMethod]
        public void FindPath_NullGraph_ReturnsEmptyPath()
        {
            var dikstraAlgorithm = new DijkstraAlgorithm() { Graph = NullGraph.Instance };

            dikstraAlgorithm.FindPath();

            Assert.IsFalse(dikstraAlgorithm.GetPath().Any());
        }

        [TestMethod]
        public void FindPath_NotNullGraph_ReturnsNotEmptyPath()
        {
            
            var startVertexPosition = new Position(x: 1, y: 1);
            var endVertexPosition = new Position(x: 9, y: 9);
            var parametres = new GraphParametres(width: 10, height: 10, obstaclePercent: 0);
            var factory = new GraphFactory(parametres);
            var graph = factory.CreateGraph(() => new TestVertex());
            Thread.Sleep(millisecondsTimeout: 230);
            graph.Start = graph[startVertexPosition];
            graph.End = graph[endVertexPosition];
            var dikstraAlgorithm = new DijkstraAlgorithm() { Graph = graph };

            dikstraAlgorithm.FindPath();

            Assert.IsTrue(dikstraAlgorithm.GetPath().Any());
        }
    }
}
