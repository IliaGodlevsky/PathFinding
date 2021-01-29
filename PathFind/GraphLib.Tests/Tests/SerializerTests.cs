using GraphLib.Coordinates;
using GraphLib.Extensions;
using GraphLib.Graphs;
using GraphLib.Graphs.Abstractions;
using GraphLib.Graphs.Factories;
using GraphLib.Graphs.Serialization;
using GraphLib.Info;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var graph2DFactory = new GraphFactory<Graph2D>(0, 15, 15);
            var graph3DFactory = new GraphFactory<Graph3D>(0, 7, 8, 9);

            graph2D = graph2DFactory.CreateGraph(CreateVertex, CreateCoordinate2D);
            graph3D = graph3DFactory.CreateGraph(CreateVertex, CreateCoordinate3D);
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

        private TestVertex CreateVertex()
        {
            return new TestVertex();
        }

        private Coordinate2D CreateCoordinate2D(IEnumerable<int> coordinates)
        {
            return new Coordinate2D(coordinates.ToArray());
        }

        private Coordinate3D CreateCoordinate3D(IEnumerable<int> coordinates)
        {
            return new Coordinate3D(coordinates.ToArray());
        }
    }
}
