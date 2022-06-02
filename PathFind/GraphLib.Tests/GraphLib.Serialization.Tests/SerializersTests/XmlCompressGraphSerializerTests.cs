using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class XmlCompressGraphSerializerTests : XmlGraphSerializerTests
    {
        protected override IGraphSerializer Serializer { get; }

        public XmlCompressGraphSerializerTests()
        {
            Serializer = new CompressGraphSerializer(base.Serializer);
        }
    }
}
