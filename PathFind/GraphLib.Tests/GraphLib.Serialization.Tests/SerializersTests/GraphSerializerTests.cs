using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using GraphLib.TestRealizations.TestFactories;
using NUnit.Framework;
using System.Collections;
using System.IO;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    internal abstract class GraphSerializerTests
    {
        protected IGraphAssemble GraphAssemble { get; }

        protected IVertexFromInfoFactory VertexFactory { get; }

        protected IVertexCostFactory CostFactory { get; }

        protected ICoordinateFactory CoordinateFactory { get; }

        protected IGraphFactory GraphFactory { get; }

        protected abstract IGraphSerializer Serializer { get; }

        protected GraphSerializerTests()
        {
            GraphAssemble = new TestGraphAssemble();
            VertexFactory = new TestVertexFromInfoFactory();
            GraphFactory = new TestGraphFactory();
            CostFactory = new TestCostFactory();
            CoordinateFactory = new TestCoordinateFactory();
        }

        public static IEnumerable TestCaseData { get; }

        static GraphSerializerTests()
        {
            TestCaseData = new TestCaseData[]
            {
                new TestCaseData(11, new[] { 250 }),
                new TestCaseData(10, new[] { 12, 15 }),
                new TestCaseData(15, new[] { 5, 6, 7 })
            };
        }

        [TestCaseSource(nameof(TestCaseData))]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(int obstaclePercent, int[] graphParams)
        {
            IGraph deserialized;
            var graph = GraphAssemble.AssembleGraph(obstaclePercent, graphParams);
            using (var stream = new MemoryStream())
            {
                Serializer.SaveGraph(graph, stream);
                stream.Seek(0, SeekOrigin.Begin);
                deserialized = Serializer.LoadGraph(stream);
            }
            Assert.Multiple(() =>
            {
                Assert.AreEqual(graph, deserialized);
                Assert.AreNotSame(graph, deserialized);
            });
        }
    }
}
