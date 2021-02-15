using GraphLib.Common.NullObjects;
using GraphLib.Extensions;
using GraphLib.Interface;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization.Tests.Factories;
using GraphLib.Serialization.Tests.Objects;
using NUnit.Framework;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Serialization.Tests
{
    [TestFixture]
    internal class GraphSerializerTests
    {
        private readonly BinaryFormatter formatter;
        private readonly TestVertexInfoSerializationConverter vertexConverter;
        private readonly TestGraphFactory graphFactory;
        private readonly TestVertexFactory vertexFactory;
        private readonly TestCoordinateFactory coordinateFactory;
        private readonly TestCostFactory costFactory;
        private readonly GraphAssembler graphAssembler;

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

        [TestCase(15, new int[] { 13, 14, 15 })]
        [TestCase(22, new int[] { 67, 32 })]
        [TestCase(66, new int[] { 17, 18, 11, 13 })]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(int obstaclePercent, int[] graphParams)
        {
            IGraph deserialized = new NullGraph();

            var graph = graphAssembler.AssembleGraph(obstaclePercent, graphParams);
            var serializer = new GraphSerializer(formatter, vertexConverter, graphFactory);

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph, stream);
                stream.Seek(0, SeekOrigin.Begin);
                deserialized = serializer.LoadGraph(stream);
            }

            Assert.IsTrue(graph.IsEqual(deserialized));
            Assert.AreNotSame(graph, deserialized);
        }
    }
}
