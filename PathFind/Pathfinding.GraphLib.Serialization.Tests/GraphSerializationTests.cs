using NUnit.Framework;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Interface.Extensions;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random.Realizations;
using System.IO;
using System.Linq;

namespace Pathfinding.GraphLib.Serialization.Tests
{
    using Assemble = GraphAssemble<TestGraph, TestVertex>;
    using Layer = ILayer<TestGraph, TestVertex>;
    using Serializer = ISerializer<TestGraph>;

    [TestFixture]
    public class GraphSerializationTests
    {
        [TestCaseSource(typeof(GraphSerializationData), nameof(GraphSerializationData.Data))]
        public void SaveLoadMethod_VariousGraphSerializers_Success(Serializer serializer, int[] dimensions)
        {
            string errorMessage = "Graph was serialized incorrectly";
            string wrongNeighbors = "Neighbors where serialized wrongly";

            using (var stream = new MemoryStream())
            {
                var assemble = GetAssemble();
                var layers = GetLayers();
                var graph = assemble.AssembleGraph(layers, dimensions);

                serializer.SerializeTo(graph, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var loaded = serializer.DeserializeFrom(stream);

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(loaded.DimensionsSizes.SequenceEqual(graph.DimensionsSizes), errorMessage);
                    Assert.IsTrue(graph.GetObstaclesCount() == loaded.GetObstaclesCount(), errorMessage);
                    Assert.IsTrue(graph.Juxtapose(loaded, (a, b) => a.Equals(b)), errorMessage);
                    Assert.IsTrue(graph.Juxtapose(loaded, CompareNeighbors), wrongNeighbors);
                });
            }
        }

        private static bool CompareNeighbors(TestVertex v1, TestVertex v2)
        {
            bool VertexEquals(IVertex first, IVertex second)
            {
                return first.Equals(second);
            }
            return v1.Neighbours.Juxtapose(v2.Neighbours, VertexEquals);
        }

        private static Assemble GetAssemble()
        {
            var graphFactory = new TestGraphFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var vertexFactory = new TestVertexFactory();
            return new Assemble(vertexFactory, coordinateFactory, graphFactory);
        }

        private static Layer[] GetLayers()
        {
            int obstaclePercent = 15;
            var random = new CongruentialRandom();
            var range = new InclusiveValueRange<int>(9, 1);
            return new Layer[]
            {
                new NeighborhoodLayer<TestGraph, TestVertex>(new MooreNeighborhoodFactory()),
                new VertexCostLayer<TestGraph, TestVertex>(new CostFactory(), range, random),
                new ObstacleLayer<TestGraph, TestVertex>(random, obstaclePercent)
            };
        }
    }
}