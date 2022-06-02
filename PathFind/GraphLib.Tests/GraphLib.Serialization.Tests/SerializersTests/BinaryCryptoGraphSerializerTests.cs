using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryCryptoGraphSerializerTests : BinaryGraphSerializerTests
    {
        protected override IGraphSerializer Serializer { get; }

        public BinaryCryptoGraphSerializerTests() : base()
        {
            Serializer = new CryptoGraphSerializer(base.Serializer);
        }
    }
}
