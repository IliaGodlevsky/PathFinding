using Algorithm.Realizations.GraphPaths;
using Algorithm.Сompanions;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using System.Linq;

namespace Algorithm.Realizations.Tests
{
    [TestFixture]
    public class GraphPathTests
    {
        private readonly IGraphAssemble graphAssemble;
        private readonly IGraph graph;
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
            graph = graphAssemble.AssembleGraph(0);
            var source = graph[expectedPraphPathCoordinates.First()];
            var target = graph[expectedPraphPathCoordinates.Last()];
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

            double pathCost = graphPath.PathCost;
            int pathLength = graphPath.PathLength;
            var pathCoordinates = graphPath.Path.Select(Coordinate).Reverse();

            Assert.AreEqual(expectedPathLength, pathLength);
            Assert.AreEqual(expectedPathCost, pathCost);
            Assert.IsTrue(pathCoordinates.Juxtapose(expectedPraphPathCoordinates));
        }

        private void FormParentVertices(ParentVertices parentVertices)
        {
            for (int i = 0; i < expectedPraphPathCoordinates.Length - 1; i++)
            {
                var childCoordinate = graph[expectedPraphPathCoordinates[i + 1]];
                var parentCoordinate = graph[expectedPraphPathCoordinates[i]];
                parentVertices.Add(childCoordinate, parentCoordinate);
            }
        }

        private ICoordinate Coordinate(IVertex vertex)
        {
            return vertex.Position;
        }
    }
}
