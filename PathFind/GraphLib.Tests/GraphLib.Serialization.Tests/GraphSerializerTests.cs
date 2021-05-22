using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphLib.TestRealizations.TestFactories;
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
        private readonly IVertexSerializationInfoConverter vertexConverter;
        private readonly IGraphFactory graphFactory;
        private readonly IGraphAssemble graphAssembler;

        public GraphSerializerTests()
        {
            formatter = new BinaryFormatter();
            graphFactory = new TestGraphFactory();
            vertexConverter = new TestVertexInfoSerializationConverter();
            graphAssembler = new TestGraphAssemble();
        }

        [TestCase(11, new[] { 8, 9, 7 })]
        [TestCase(22, new[] { 15, 15 })]
        [TestCase(66, new[] { 4, 3, 4, 2 })]
        [TestCase(44, new[] { 4, 3, 4, 2, 2 })]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(
            int obstaclePercent, int[] graphParams)
        {
            IGraph deserialized;
            var graph = graphAssembler.AssembleGraph(obstaclePercent, graphParams);
            var serializer = new GraphSerializer(formatter, vertexConverter, graphFactory);

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
