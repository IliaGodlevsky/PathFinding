using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Realizations.CoordinateFactories;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
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
                yield return GenerateXmlGraphSerializer(5, 10);
                yield return GenerateXmlGraphSerializer(4, 5, 6);
                yield return GenerateXmlGraphSerializer(3, 3, 5, 5);
                yield return GenerateBinaryGraphSerializer(5, 10);
                yield return GenerateBinaryGraphSerializer(4, 5, 6);
                yield return GenerateBinaryGraphSerializer(3, 3, 5, 5);
                yield return GenerateBinaryCryptoGraphSerializer(5, 10);
                yield return GenerateBinaryCryptoGraphSerializer(4, 5, 6);
                yield return GenerateBinaryCryptoGraphSerializer(3, 3, 5, 5);
                yield return GenerateBinaryCompressGraphSerializer(5, 10);
                yield return GenerateBinaryCompressGraphSerializer(4, 5, 6);
                yield return GenerateBinaryCompressGraphSerializer(3, 3, 5, 5);
            }
        }

        private static TestCaseData GenerateXmlGraphSerializer(params int[] dimensions)
        {
            var serializer = GetSerializer<XmlGraphSerializer<TestVertex>>();
            return new TestCaseData(serializer, dimensions);
        }

        private static TestCaseData GenerateBinaryGraphSerializer(params int[] dimensions)
        {
            var serializer = GetSerializer<BinaryGraphSerializer<TestVertex>>();
            return new TestCaseData(serializer, dimensions);
        }

        private static TestCaseData GenerateBinaryCryptoGraphSerializer(params int[] dimensions)
        {
            var serializer = GetSerializer<BinaryGraphSerializer<TestVertex>>();
            var decorator = new CryptoSerializer<IGraph<TestVertex>>(serializer);
            return new TestCaseData(decorator, dimensions);
        }

        private static TestCaseData GenerateBinaryCompressGraphSerializer(params int[] dimensions)
        {
            var serializer = GetSerializer<BinaryGraphSerializer<TestVertex>>();
            var decorator = new CompressSerializer<IGraph<TestVertex>>(serializer);
            return new TestCaseData(decorator, dimensions);
        }

        private static TSerializer GetSerializer<TSerializer>()
            where TSerializer : GraphSerializer<TestVertex>
        {
            var vertexFactory = new TestVertexFromInfoFactory();
            var graphFactory = new GraphFactory<TestVertex>();
            var costFactory = new TestCostFactory();
            var coordinateFactory = new CoordinateFactory();
            return (TSerializer)Activator.CreateInstance(typeof(TSerializer),
                vertexFactory, graphFactory, costFactory, coordinateFactory);
        }
    }
}
