using NUnit.Framework;
using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Serialization.Core.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Extensions;
using System.IO;

namespace Pathfinding.GraphLib.Serialization.Tests
{
    using Serializer = IGraphSerializer<TestGraph, TestVertex>;
    using Assemble = GraphAssemble<TestGraph, TestVertex>;
    using Layer = ILayer<TestGraph, TestVertex>;

    [TestFixture]
    public class GraphSerializationTests
    {
        [TestCaseSource(typeof(GraphSerializationData), nameof(GraphSerializationData.Data))]
        public void SaveLoadMethod_VariousGraphSerializers_Success(Serializer serializer, Assemble assemble,
            Layer[] layers, int[] dimensions)
        {
            string errorMessage = "Graph was serialized incorrectly";
            string wrongNeighbors = "Neighbors where serialized wrongly";

            using (var stream = new MemoryStream())
            {
                var graph = assemble.AssembleGraph(layers, dimensions);
                serializer.SaveGraph(graph, stream);
                stream.Seek(0, SeekOrigin.Begin);
                var loaded = serializer.LoadGraph(stream);
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(graph.Equals(loaded), errorMessage);
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
    }
}