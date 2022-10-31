using Algorithm.Realizations.GraphPaths;
using Algorithm.Сompanions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace Algorithm.Realizations.Tests
{
    [TestFixture]
    public class GraphPathTests
    {
        private readonly IGraphAssemble<Graph2D<TestVertex>, TestVertex> graphAssemble;
        private readonly Graph2D<TestVertex> graph;
        private readonly ICoordinate[] expectedPraphPathCoordinates;
        private readonly IEndPoints endPoints;
        private readonly ParentVertices parentVertices;

        public GraphPathTests()
        {
            graphAssemble = new TestGraph2DAssemble();
            expectedPraphPathCoordinates = new ICoordinate[]
            {
                new TestCoordinate(0,0), // cost: 1
                new TestCoordinate(0,1), //5
                new TestCoordinate(1,2), //8
                new TestCoordinate(1,3), //1
                new TestCoordinate(2,4), //3
                new TestCoordinate(3,5), //2
                new TestCoordinate(3,6), //1
                new TestCoordinate(4,7)  //5
            };
            graph = graphAssemble.AssembleGraph();
            var source = graph.Get(expectedPraphPathCoordinates.First());
            var target = graph.Get(expectedPraphPathCoordinates.Last());
            endPoints = new TestEndPoints(source, target);
            parentVertices = new ParentVertices();
            FormParentVertices(parentVertices);
        }

        [Test]
        public void Path_ValidParentVertices_ReturnsFullPath()
        {
            int expectedPathLength = expectedPraphPathCoordinates.Length - 1;
            const int expectedPathCost = 25;
            var graphPath = new GraphPath(parentVertices, endPoints);

            double pathCost = graphPath.Cost;
            int pathLength = graphPath.Count;
            var pathCoordinates = graphPath.Select(vertex => vertex.Position).Reverse();

            Assert.AreEqual(expectedPathLength, pathLength);
            Assert.AreEqual(expectedPathCost, pathCost);
            Assert.IsTrue(pathCoordinates.Juxtapose(expectedPraphPathCoordinates));
        }

        private void FormParentVertices(ParentVertices parentVertices)
        {
            for (int i = 0; i < expectedPraphPathCoordinates.Length - 1; i++)
            {
                var childCoordinate = graph.Get(expectedPraphPathCoordinates[i + 1]);
                var parentCoordinate = graph.Get(expectedPraphPathCoordinates[i]);
                parentVertices.Add(childCoordinate, parentCoordinate);
            }
        }
    }
}
