using GraphLib.Coordinates.Infrastructure.Factories;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.Graphs.Serialization;
using GraphLib.Info;
using NUnit.Framework;
using System.IO;

namespace GraphLib.Tests.Tests
{
    [TestFixture]
    public class SerializerTests
    {
        private IGraph graph2D;
        private IGraph graph3D;

        [SetUp]
        public void SetUp()
        {
            var vertexFactory = new TestVertexFactory();
            var coordinate2DFactory = new Coordinate2DFactory();
            var coordinate3DFactory = new Coordinate3DFactory();
            var graph2DFactory = new GraphFactory<Graph2D>(vertexFactory, coordinate2DFactory);
            var graph3DFactory = new GraphFactory<Graph3D>(vertexFactory, coordinate3DFactory);

            graph2D = graph2DFactory.CreateGraph(0, 15, 15);
            graph3D = graph3DFactory.CreateGraph(0, 7, 8, 9);
        }

        [Test]
        public void SaveGraph_LoadGraph_Graph2D_ReturnsEqualGraph()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer<Graph2D>();

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph2D, stream);
                stream.Position = 0;
                deserialized = serializer.LoadGraph(stream, CreateVertex);
            }

            Assert.IsTrue(graph2D.IsEqual(deserialized));
            Assert.AreNotSame(graph2D, deserialized);
        }

        [Test]
        public void SaveGraph_LoadGraph_Graph3D_ReturnsEqualGraph()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer<Graph3D>();

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph3D, stream);
                stream.Position = 0;
                deserialized = serializer.LoadGraph(stream, CreateVertex);
            }

            Assert.IsTrue(graph3D.IsEqual(deserialized));
            Assert.AreNotSame(graph3D, deserialized);
        }

        [Test]
        public void SaveGraph_LoadGraph_WrongGraphType_ReturnsNullGraph()
        {
            IGraph deserialized = new NullGraph();
            var serializer = new GraphSerializer<Graph2D>();

            using (var stream = new MemoryStream())
            {
                serializer.SaveGraph(graph3D, stream);
                stream.Position = 0;
                deserialized = serializer.LoadGraph(stream, CreateVertex);
            }

            Assert.IsTrue(deserialized.IsDefault);
        }

        private TestVertex CreateVertex(VertexSerializationInfo info)
        {
            return new TestVertex(info);
        }
    }
}
