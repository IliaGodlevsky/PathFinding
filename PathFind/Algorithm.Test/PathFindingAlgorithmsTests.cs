using Algorithm.PathFindingAlgorithms;
using GraphLib;
using GraphLib.Coordinates;
using GraphLib.Graphs;
using GraphLib.Graphs.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Algorithm.Test
{
    [TestClass]
    public class PathFindingAlgorithmsTests
    {
        [Ignore]
        private Graph2D CreateGraph2D()
        {
            var factory = new GraphFactory<Graph2D>(0, 13, 13);
            var graph = (Graph2D)factory.CreateGraph(() => new TestVertex(),
                (coordinates) => new Coordinate2D(coordinates.ToArray()));
            graph.Start = graph.First();
            graph.End = graph.Last();
            return graph;
        }

        [Ignore]
        private Graph3D CreateGraph3D()
        {
            var factory = new GraphFactory<Graph3D>(0, 12, 13, 10);
            var graph = (Graph3D)factory.CreateGraph(() => new TestVertex(),
                (coordinates) => new Coordinate3D(coordinates.ToArray()));
            graph.Start = graph.First();
            graph.End = graph.Last();
            return graph;
        }

        [Ignore]
        private Graph4D CreateGraph4D()
        {
            var factory = new GraphFactory<Graph4D>(0, 7, 7, 5, 5);
            var graph = (Graph4D)factory.CreateGraph(() => new TestVertex(),
                (coordinates) => new Coordinate4D(coordinates.ToArray()));
            graph.Start = graph.First();
            graph.End = graph.Last();
            return graph;
        }

        [TestMethod]
        public void LeeAlgorithm_Should_FindPath_When_GraphAs2D()
        {
            var graph = CreateGraph2D();
            var leeAlgorithm = new LeeAlgorithm(graph);
            var path = new Path();

            leeAlgorithm.FindPath();            
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void LeeAlgorithm_Should_FindPath_When_GraphAs3D()
        {
            var graph = CreateGraph3D();
            var leeAlgorithm = new LeeAlgorithm(graph);
            var path = new Path();

            leeAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void BestFirstLeeAlgorithm_Should_FindPath_When_GraphAs2D()
        {
            var graph = CreateGraph2D();
            var bestFirstLeeAlgorithm = new BestFirstLeeAlgorithm(graph);
            var path = new Path();

            bestFirstLeeAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void BestFirstLeeAlgorithm_Should_FindPath_When_GraphAs3D()
        {
            var graph = CreateGraph3D();
            var bestFirstLeeAlgorithm = new BestFirstLeeAlgorithm(graph);
            var path = new Path();

            bestFirstLeeAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void DijkstraAlgorithm_Should_FindPath_When_GraphAs2D()
        {
            var graph = CreateGraph2D();
            var dijkstraAlgorithm = new DijkstraAlgorithm(graph);
            var path = new Path();

            dijkstraAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void DijkstraAlgorithm_Should_FindPath_When_GraphAs3D()
        {
            var graph = CreateGraph3D();
            var dijkstraAlgorithm = new DijkstraAlgorithm(graph);
            var path = new Path();

            dijkstraAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void DijkstraAlgorithm_Should_FindPath_When_GraphAs4D()
        {
            var graph = CreateGraph4D();
            var dijkstraAlgorithm = new DijkstraAlgorithm(graph);
            var path = new Path();

            dijkstraAlgorithm.FindPath();
            path.ExtractPath(graph);

            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }
    }
}
