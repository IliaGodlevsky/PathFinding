using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryCompressGraphSerializerTests : BinaryGraphSerializerTests
    {
        protected override IGraphSerializer Serializer { get; }

        public BinaryCompressGraphSerializerTests()
        {
            Serializer = new CompressGraphSerializer(base.Serializer);
        }
    }
}
