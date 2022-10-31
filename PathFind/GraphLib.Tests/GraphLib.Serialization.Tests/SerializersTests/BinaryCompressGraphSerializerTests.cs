using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers.Decorators;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryCompressGraphSerializerTests : BinaryGraphSerializerTests
    {
        protected override IGraphSerializer<TestGraph, TestVertex> Serializer { get; }

        public BinaryCompressGraphSerializerTests()
        {
            Serializer = new CompressGraphSerializer<TestGraph, TestVertex>(base.Serializer);
        }
    }
}
