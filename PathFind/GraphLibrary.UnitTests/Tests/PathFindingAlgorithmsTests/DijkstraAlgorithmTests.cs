using System.Linq;
using GraphLibrary.Extensions.CustomTypeExtensions;
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
            var startVertexPosition = new Position(1, 1);

            var endVertexPosition = new Position(10, 10);

            var parametres = new GraphParametres(width: 25, height: 25, obstaclePercent: 0);
            var factory = new GraphFactory.GraphFactory(parametres);

            var graph = factory.GetGraph(() => new TestVertex());

            graph.Start = graph[startVertexPosition.X, startVertexPosition.Y];
            graph.End = graph[endVertexPosition.X,endVertexPosition.Y];

            var dikstraAlgorithm = new DijkstraAlgorithm() { Graph = graph };

            dikstraAlgorithm.FindPath();

            Assert.IsTrue(dikstraAlgorithm.GetPath().Any());
        }
    }
}
