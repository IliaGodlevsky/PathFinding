﻿using GraphLib.Base;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization.Tests.Factories;
using GraphLib.Serialization.Tests.Objects;
using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Serialization.Tests
{
    [TestFixture]
    internal class GraphSerializerTests
    {
        private readonly IFormatter formatter;
        private readonly TestVertexInfoSerializationConverter vertexConverter;
        private readonly IGraphFactory graphFactory;
        private readonly IVertexFactory vertexFactory;
        private readonly ICoordinateFactory coordinateFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly IGraphAssembler graphAssembler;

        public GraphSerializerTests()
        {
            formatter = new BinaryFormatter();
            graphFactory = new TestGraphFactory();
            vertexConverter = new TestVertexInfoSerializationConverter();
            vertexFactory = new TestVertexFactory();
            coordinateFactory = new TestCoordinateFactory();
            costFactory = new TestCostFactory();
            graphAssembler = new GraphAssembler(
                vertexFactory,
                coordinateFactory,
                graphFactory,
                costFactory);
        }

        [TestCase(15, new int[] { 11, 9, 10 })]
        [TestCase(22, new int[] { 25, 25 })]
        [TestCase(66, new int[] { 4, 3, 7, 5 })]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(
            int obstaclePercent, int[] graphParams)
        {
            var deserialized = BaseGraph.NullGraph;
            var graph = graphAssembler.AssembleGraph(
                obstaclePercent, graphParams);
            var serializer = new GraphSerializer(
                formatter, vertexConverter, graphFactory);

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph, stream);
                stream.Seek(0, SeekOrigin.Begin);
                deserialized = serializer.LoadGraph(stream);
            }

            Assert.AreEqual(graph, deserialized);
            Assert.AreNotSame(graph, deserialized);
        }
    }
}