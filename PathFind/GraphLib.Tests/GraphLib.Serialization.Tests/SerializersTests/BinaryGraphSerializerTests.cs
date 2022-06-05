using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryGraphSerializerTests : GraphSerializerTests
    {
        protected override IGraphSerializer Serializer { get; }

        public BinaryGraphSerializerTests()
        {
            Serializer = new BinaryGraphSerializer(VertexFactory, GraphFactory, CostFactory, CoordinateFactory);
        }
    }
}
