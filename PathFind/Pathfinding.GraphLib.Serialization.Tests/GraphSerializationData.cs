using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System;
using System.Collections;

namespace Pathfinding.GraphLib.Serialization.Tests
{
    internal static class GraphSerializationData
    {
        public static IEnumerable Data
        {
            get
            {
                yield return GenerateXmlGraphSerializer();
                yield return GenerateBinaryGraphSerializer();
                yield return GenerateCryptoGraphSerializer();
                yield return CreateCompressGraphSerializer();
            }
        }

        private static TestCaseData GenerateXmlGraphSerializer()
        {
            var serializer = GetSerializer<XmlGraphSerializer<TestGraph, TestVertex>>();
            return new TestCaseData(serializer);
        }

        private static TestCaseData GenerateBinaryGraphSerializer()
        {
            var serializer = GetSerializer<BinaryGraphSerializer<TestGraph, TestVertex>>();
            return new TestCaseData(serializer);
        }

        private static TestCaseData GenerateCryptoGraphSerializer()
        {
            var serializer = GetSerializer<XmlGraphSerializer<TestGraph, TestVertex>>();
            var decorator = new CryptoGraphSerializer<TestGraph, TestVertex>(serializer);
            return new TestCaseData(decorator);
        }

        private static TestCaseData CreateCompressGraphSerializer()
        {
            var serializer = GetSerializer<XmlGraphSerializer<TestGraph, TestVertex>>();
            var decorator = new CompressGraphSerializer<TestGraph, TestVertex>(serializer);
            return new TestCaseData(decorator);
        }

        private static TSerializer GetSerializer<TSerializer>()
            where TSerializer : GraphSerializer<TestGraph, TestVertex>
        {
            var vertexFactory = new TestVertexFromInfoFactory();
            var graphFactory = new TestGraphFactory();
            var costFactory = new TestCostFactory();
            var coordinateFactory = new TestCoordinateFactory();
            return (TSerializer)Activator.CreateInstance(typeof(TSerializer), vertexFactory, 
                graphFactory, costFactory, coordinateFactory);
        }
    }
}
