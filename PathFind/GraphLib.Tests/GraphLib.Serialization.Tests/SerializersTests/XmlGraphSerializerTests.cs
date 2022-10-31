using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class XmlGraphSerializerTests : GraphSerializerTests
    {
        protected override IGraphSerializer<TestGraph, TestVertex> Serializer { get; }

        public XmlGraphSerializerTests()
        {
            Serializer = new XmlGraphSerializer<TestGraph, TestVertex>(VertexFactory, GraphFactory, CostFactory, CoordinateFactory);
        }
    }
}
