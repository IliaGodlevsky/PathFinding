using GraphLib.Serialization.Interfaces;
using GraphLib.Serialization.Serializers;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    internal class BinaryGraphSerializerTests : GraphSerializerTests
    {
        protected override IGraphSerializer<TestGraph, TestVertex> Serializer { get; }

        public BinaryGraphSerializerTests()
        {
            Serializer = new BinaryGraphSerializer<TestGraph, TestVertex>(VertexFactory, GraphFactory, CostFactory, CoordinateFactory);
        }
    }
}
