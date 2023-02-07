using GraphLib.Serialization.Serializers.Decorators;
using NUnit.Framework;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers;
using Pathfinding.GraphLib.Serialization.Core.Realizations.Serializers.Decorators;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.ValueRange;
using Shared.Random.Realizations;
using System;
using System.Collections;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;

namespace Pathfinding.GraphLib.Serialization.Tests
{
    internal static class GraphSerializationData
    {
        public static IEnumerable Data
        {
            get
            {
                yield return GenerateXmlGraphSerializer(35, 5, 10);
                yield return GenerateXmlGraphSerializer(10, 4, 5, 6);
                yield return GenerateXmlGraphSerializer(25, 3, 3, 5, 5);
                yield return GenerateBinaryGraphSerializer(35, 5, 10);
                yield return GenerateBinaryGraphSerializer(10, 4, 5, 6);
                yield return GenerateBinaryGraphSerializer(25, 3, 3, 5, 5);
                yield return GenerateBinaryCryptoGraphSerializer(35, 5, 10);
                yield return GenerateBinaryCryptoGraphSerializer(10, 4, 5, 6);
                yield return GenerateBinaryCryptoGraphSerializer(25, 3, 3, 5, 5);
                yield return GenerateBinaryCompressGraphSerializer(35, 5, 10);
                yield return GenerateBinaryCompressGraphSerializer(10, 4, 5, 6);
                yield return GenerateBinaryCompressGraphSerializer(25, 3, 3, 5, 5);
            }
        }

        private static TestCaseData GenerateXmlGraphSerializer(int obstaclePercent, params int[] dimensions)
        {           
            var assemble = GetAssemble();
            var layers = GetLayers(obstaclePercent);
            var serializer = GetSerializer<XmlGraphSerializer<TestGraph, TestVertex>>();
            return new TestCaseData(serializer, assemble, layers, dimensions);
        }

        private static TestCaseData GenerateBinaryGraphSerializer(int obstaclePercent, params int[] dimensions)
        {
            var assemble = GetAssemble();
            var layers = GetLayers(obstaclePercent);
            var serializer = GetSerializer<BinaryGraphSerializer<TestGraph, TestVertex>>();
            return new TestCaseData(serializer, assemble, layers, dimensions);
        }

        private static TestCaseData GenerateBinaryCryptoGraphSerializer(int obstaclePercent, params int[] dimensions)
        {
            var assemble = GetAssemble();
            var layers = GetLayers(obstaclePercent);
            var serializer = GetSerializer<BinaryGraphSerializer<TestGraph, TestVertex>>();
            var decorator = new CryptoGraphSerializer<TestGraph, TestVertex>(serializer);
            return new TestCaseData(decorator, assemble, layers, dimensions);
        }

        private static TestCaseData GenerateBinaryCompressGraphSerializer(int obstaclePercent, params int[] dimensions)
        {
            var assemble = GetAssemble();
            var layers = GetLayers(obstaclePercent);
            var serializer = GetSerializer<BinaryGraphSerializer<TestGraph, TestVertex>>();
            var decorator = new CompressGraphSerializer<TestGraph, TestVertex>(serializer);
            return new TestCaseData(decorator, assemble, layers, dimensions);
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

        private static GraphAssemble<TestGraph, TestVertex> GetAssemble()
        {
            var graphFactory = new TestGraphFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var vertexFactory = new TestVertexFactory();
            return new GraphAssemble<TestGraph, TestVertex>(vertexFactory,
                coordinateFactory, graphFactory);
        }

        private static ILayer<TestGraph, TestVertex>[] GetLayers(int obstaclePercent)
        {
            var random = new PseudoRandom();
            var range = new InclusiveValueRange<int>(9, 1);
            return new ILayer<TestGraph, TestVertex>[]
            {
                new NeighborhoodLayer<TestGraph, TestVertex>(new MooreNeighborhoodFactory()),
                new VertexCostLayer<TestGraph, TestVertex>(new CostFactory(), range, random),
                new ObstacleLayer<TestGraph, TestVertex>(random, 15)
            };
        }
    }
}
