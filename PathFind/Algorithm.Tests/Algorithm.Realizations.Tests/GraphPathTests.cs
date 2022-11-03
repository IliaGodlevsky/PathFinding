using Algorithm.Realizations.GraphPaths;
using Common.Extensions.EnumerableExtensions;
using GraphLib.Extensions;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Graphs;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using GraphLib.Utility;
using NUnit.Framework;
using System.Collections.Generic;
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
        private readonly Dictionary<ICoordinate, IVertex> traces;

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
            traces = new Dictionary<ICoordinate, IVertex>(new CoordinateEqualityComparer());
        }

        [Test]
        public void Path_ValidTraces_ReturnsFullPath()
        {
            int expectedPathLength = expectedPraphPathCoordinates.Length - 1;
            const int expectedPathCost = 25;
            var graphPath = new GraphPath(GenerateTraces(), endPoints.Target);

            double pathCost = graphPath.Cost;
            int pathLength = graphPath.Count;
            var pathCoordinates = graphPath.Reverse();

            Assert.AreEqual(expectedPathLength, pathLength);
            Assert.AreEqual(expectedPathCost, pathCost);
            Assert.IsTrue(pathCoordinates.Juxtapose(expectedPraphPathCoordinates));
        }

        private IReadOnlyDictionary<ICoordinate, IVertex> GenerateTraces()
        {
            var traces = new Dictionary<ICoordinate, IVertex>(new CoordinateEqualityComparer());

            for (int i = 0; i < expectedPraphPathCoordinates.Length - 1; i++)
            {
                var childCoordinate = graph.Get(expectedPraphPathCoordinates[i + 1]);
                var parentCoordinate = graph.Get(expectedPraphPathCoordinates[i]);
                traces[childCoordinate.Position] = parentCoordinate;
            }
            return traces.ToReadOnly();
        }
    }
}
