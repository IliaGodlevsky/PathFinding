using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers.Decorators;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryCryptoGraphSerializerTests : BinaryGraphSerializerTests
    {
        protected override IGraphSerializer<TestGraph, TestVertex> Serializer { get; }

        public BinaryCryptoGraphSerializerTests() : base()
        {
            Serializer = new CryptoGraphSerializer<TestGraph, TestVertex>(base.Serializer);
        }
    }
}
