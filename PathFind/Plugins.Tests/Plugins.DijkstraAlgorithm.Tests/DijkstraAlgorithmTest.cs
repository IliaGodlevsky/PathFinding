using Algorithm.Realizations.StepRules;
using GraphLib.Common.NullObjects;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization;
using GraphLib.Serialization.Interfaces;
using NUnit.Framework;
using Plugins.DijkstraAlgorithm.Tests.Objects.Factories;
using Plugins.DijkstraAlgorithm.Tests.Objects.TestObjects;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using DijkstrasAlgorithm = Plugins.DijkstraALgorithm.DijkstraAlgorithm;

namespace Plugins.DijkstraAlgorithm.Tests
{
    [TestFixture]
    public class DijkstraAlgorithmTest
    {
        private readonly ICoordinateRadarFactory radarFactory;
        private readonly IGraphFactory graphFactory;
        private readonly IGraphSerializer graphSerializer;
        private readonly IVertexSerializationInfoConverter infoConverter;
        private readonly IFormatter formatter;

        public DijkstraAlgorithmTest()
        {
            radarFactory = new CoordinateAroundRadarFactory();
            graphFactory = new TestGraphFactory();
            formatter = new BinaryFormatter();
            infoConverter = new TestVertexSerializationInfoConverter(radarFactory);

            graphSerializer = new GraphSerializer(formatter, infoConverter, graphFactory);
        }

        #region Test Methods

        [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.TestData))]
        public void FindPath_EndpointsBelongToGraphAndStepRuleIsDefault_ReturnsShortestPath(
            string testGraph, int expectedCost, int expectedLength)
        {
            IGraph graph;
            using (var stream = new FileStream(testGraph, FileMode.Open))
            {
                graph = graphSerializer.LoadGraph(stream);
            }
            var endPoints = new TestEndPoints(graph.Vertices.First(), graph.Vertices.Last());
            var algorithm = new DijkstrasAlgorithm(graph, endPoints, new DefaultStepRule());

            var graphPath = algorithm.FindPath();

            Assert.AreEqual(graphPath.Path.Count(), expectedLength);
            Assert.AreEqual(expectedCost, graphPath.PathCost);
        }

        [TestCaseSource(typeof(TestDataSource), nameof(TestDataSource.TestGraphPaths))]
        public void FindPath_EndPointsDoesntBelongToGraph_TrowsArgumentException(string testGraph)
        {
            IGraph graph;
            using (var stream = new FileStream(testGraph, FileMode.Open))
            {
                graph = graphSerializer.LoadGraph(stream);
            }
            var endPoints = new TestEndPoints(new NullVertex(), new NullVertex());

            var algorithm = new DijkstrasAlgorithm(graph, endPoints);

            Assert.Throws<ArgumentException>(() => algorithm.FindPath());
        }

        #endregion
    }
}