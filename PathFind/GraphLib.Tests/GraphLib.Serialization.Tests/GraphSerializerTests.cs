using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories;
using GraphLib.Serialization.Exceptions;
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
        private readonly ICoordinateFactory notSerializableCoordinateFactory;
        private readonly IVertexCostFactory costFactory;
        private readonly ICoordinateRadarFactory radarFactory;
        private IGraphAssembler graphAssembler;

        public GraphSerializerTests()
        {
            formatter = new BinaryFormatter();
            graphFactory = new TestGraphFactory();
            radarFactory = new CoordinateAroundRadarFactory();
            vertexConverter = new TestVertexInfoSerializationConverter(radarFactory);
            vertexFactory = new TestVertexFactory();
            notSerializableCoordinateFactory = new NotSerializableCoordinateFactory();
            coordinateFactory = new TestCoordinateFactory();
            costFactory = new TestCostFactory();

        }



        [TestCase(15, new[] { 8, 9, 7 })]
        [TestCase(22, new[] { 15, 15 })]
        [TestCase(66, new[] { 4, 3, 4, 2 })]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(
            int obstaclePercent, int[] graphParams)
        {
            graphAssembler = new GraphAssembler(
                vertexFactory,
                coordinateFactory,
                graphFactory,
                costFactory,
                radarFactory);

            IGraph deserialized;
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

        [TestCase(22, new[] { 14, 11 })]
        public void SaveGraph_LoadGraph_NotSerializableCoordinate_ThrowsCantSerializeGraphException(
            int obstaclePercent, int[] graphParams)
        {
            graphAssembler = new GraphAssembler(
                vertexFactory,
                notSerializableCoordinateFactory,
                graphFactory,
                costFactory,
                radarFactory);

            var graph = graphAssembler.AssembleGraph(
                obstaclePercent, graphParams);
            var serializer = new GraphSerializer(
                formatter, vertexConverter, graphFactory);

            Assert.Throws<CantSerializeGraphException>(() =>
            {
                using var stream = new MemoryStream();
                {
                    serializer.SaveGraph(graph, stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    serializer.LoadGraph(stream);
                }
            });
        }
    }
}
