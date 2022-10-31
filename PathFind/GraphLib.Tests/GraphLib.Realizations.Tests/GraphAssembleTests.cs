using Autofac;
using Autofac.Extras.Moq;
using GraphLib.Extensions;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.Factories.GraphAssembles;
using GraphLib.Realizations.Neighbourhoods;
using GraphLib.Realizations.Tests.Extenions;
using GraphLib.TestRealizations;
using GraphLib.TestRealizations.TestFactories;
using GraphLib.TestRealizations.TestObjects;
using NUnit.Framework;
using Random.Interface;
using Random.Realizations.Generators;
using System;
using System.Linq;

namespace GraphLib.Realizations.Tests
{
    [TestFixture]
    public class GraphAssemblerTests
    {
        private void RegisterNullRandomNumberGenerator(ContainerBuilder builder)
        {
            builder.Register(container => NullRandom.Interface).As<IRandom>().SingleInstance();
        }

        private void RegisterPseudoRandomNumberGenerator(ContainerBuilder builder)
        {
            builder.RegisterType<PseudoRandom>().As<IRandom>().SingleInstance();
            builder.RegisterType<TestCostFactory>().As<IVertexCostFactory>().SingleInstance();
        }

        [TestCase(15, new int[] { 100 })]
        [TestCase(12, new int[] { 10, 15 })]
        [TestCase(12, new int[] { 7, 10, 7 })]
        public void AssembleGraph_ReturnsValidGraph(int obstaclePercent, int[] dimensionSizes)
        {
            using (var mock = AutoMock.GetLoose(RegisterPseudoRandomNumberGenerator))
            {
                mock.MockCoordinateFactory(x => new TestCoordinate(x));
                mock.MockNeighbourhoodFactory(x => new MooreNeighborhood(x));
                mock.MockVertexFactory<TestVertex>(c => new TestVertex(c));
                mock.MockGraphFactory<TestGraph, TestVertex>((v, d) => new TestGraph(v, d));

                var assemble = mock.Create<GraphAssemble<TestGraph, TestVertex>>();
                var graph = assemble.AssembleGraph(obstaclePercent, dimensionSizes);

                Assert.Multiple(() =>
                {
                    Assert.IsTrue(graph.DimensionsSizes.SequenceEqual(dimensionSizes));
                    Assert.AreEqual(obstaclePercent, graph.GetObstaclePercent());
                    Assert.IsTrue(graph.Distinct().Count() == graph.Count);
                });
            }
        }
    }
}
