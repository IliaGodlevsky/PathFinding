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
        [TestMethod]
        public void LeeAlgorithmTest_Graph2D_PathNotEmpty()
        {
            var factory = new GraphFactory<Graph2D>(0, 25, 25);

            var graph = factory.CreateGraph(() => new TestVertex(),
                (coordinates) => new Coordinate2D(coordinates.ToArray()));

            graph.Start = graph.First();
            graph.End = graph.Last();

            var leeAlgorithm = new LeeAlgorithm(graph);
            var path = new Path();




            leeAlgorithm.FindPath();            
            path.ExtractPath(graph);




            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }

        [TestMethod]
        public void LeeAlgorithmTest_Graph3D_PathNotEmpty()
        {
            var factory = new GraphFactory<Graph3D>(0, 15, 20, 5);

            var graph = factory.CreateGraph(() => new TestVertex(),
                (coordinates) => new Coordinate3D(coordinates.ToArray()));

            graph.Start = graph.First();
            graph.End = graph.Last();

            var leeAlgorithm = new LeeAlgorithm(graph);
            var path = new Path();




            leeAlgorithm.FindPath();
            path.ExtractPath(graph);




            Assert.IsTrue(!graph.Start.IsDefault && graph.Start.IsVisited);
            Assert.IsTrue(!graph.End.IsDefault && graph.End.IsVisited);
            Assert.IsTrue(path.Any());
        }
    }
}
