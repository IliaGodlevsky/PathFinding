using Autofac;
using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Serialization.Interfaces;
using NUnit.Framework;
using System.Collections;
using System.IO;

namespace GraphLib.Serialization.Tests.SerializersTests
{
    [TestFixture]
    public abstract class BaseGraphSerializerTests
    {
        public static IEnumerable TestCaseData { get; }

        static BaseGraphSerializerTests()
        {
            TestCaseData = new TestCaseData[]
            {
                new TestCaseData(11, new[] { 25 }),
                new TestCaseData(22, new[] { 7, 8 }),
                new TestCaseData(66, new[] { 4, 5, 6 })
            };
        }

        protected abstract IContainer Container { get; }

        [TestCaseSource(nameof(TestCaseData))]
        public void SaveGraph_LoadGraph_ReturnsEqualGraph(int obstaclePercent, int[] graphParams)
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var assembler = scope.Resolve<IGraphAssemble>();
                var serializer = scope.Resolve<IGraphSerializer>();
                IGraph deserialized;
                var graph = assembler.AssembleGraph(obstaclePercent, graphParams);

                using (var stream = new MemoryStream())
                {
                    serializer.SaveGraph(graph, stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    deserialized = serializer.LoadGraph(stream);
                }

                Assert.Multiple(() =>
                {
                    Assert.AreEqual(graph, deserialized);
                    Assert.AreNotSame(graph, deserialized);
                });
            }
        }
    }
}
