using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class XmlGraphSerializerTests : GraphSerializerTests
    {
        protected override IGraphSerializer Serializer { get; }

        public XmlGraphSerializerTests()
        {
            Serializer = new XmlGraphSerializer(VertexFactory, GraphFactory, CostFactory, CoordinateFactory);
        }
    }
}
