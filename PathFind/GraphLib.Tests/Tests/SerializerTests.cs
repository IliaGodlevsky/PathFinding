using GraphLib.Extensions;
using GraphLib.Factories;
using GraphLib.Interface;
using GraphLib.NullObjects;
using GraphLib.Serialization;
using GraphLib.Tests.TestInfrastructure;
using NUnit.Framework;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GraphLib.Tests.Tests
{
    [TestFixture]
    public class SerializerTests
    {
        private IGraph graph2D;
        private IGraph graph3D;
        private IGraphFactory graph2DFactory;
        private IGraphFactory graph3DFactory;
        private IFormatter formatter;
        private TestVertexInfoSerializationConverter vertexConverter;

        [SetUp]
        public void SetUp()
        {
            formatter = new BinaryFormatter();
            vertexConverter = new TestVertexInfoSerializationConverter();

            graph2DFactory = new Graph2DFactory();
            graph3DFactory = new Graph3DFactory();
            var vertexFactory = new TestVertexFactory();
            var coordinate2DFactory = new Coordinate2DFactory();
            var coordinate3DFactory = new Coordinate3DFactory();
            var graph2DFiller = new GraphAssembler(vertexFactory, coordinate2DFactory, graph2DFactory);
            var graph3DFiller = new GraphAssembler(vertexFactory, coordinate3DFactory, graph3DFactory);

            graph2D = graph2DFiller.AssembleGraph(0, 15, 15);
            graph3D = graph3DFiller.AssembleGraph(0, 7, 8, 9);
        }

        [Test]
        public void SaveGraph_LoadGraph_Graph2D_ReturnsEqualGraph()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer(formatter, vertexConverter, graph2DFactory);

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph2D, stream);
                stream.Position = 0;
                deserialized = serializer.LoadGraph(stream);
            }

            Assert.IsTrue(graph2D.IsEqual(deserialized));
            Assert.AreNotSame(graph2D, deserialized);
        }

        [Test]
        public void SaveGraph_LoadGraph_Graph3D_ReturnsEqualGraph()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer(formatter, vertexConverter, graph3DFactory);

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph3D, stream);
                stream.Position = 0;
                deserialized = serializer.LoadGraph(stream);
            }

            Assert.IsTrue(graph3D.IsEqual(deserialized));
            Assert.AreNotSame(graph3D, deserialized);
        }

        [Test]
        public void SaveGraph_LoadGraph_WrongGraphType_ThrowsArgumentOutOfRangeException()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer(formatter, vertexConverter, graph2DFactory);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                using (var stream = new MemoryStream())
                {
                    serializer.SaveGraph(graph3D, stream);
                    stream.Position = 0;
                    deserialized = serializer.LoadGraph(stream);
                }
            });           
        }
    }
}
