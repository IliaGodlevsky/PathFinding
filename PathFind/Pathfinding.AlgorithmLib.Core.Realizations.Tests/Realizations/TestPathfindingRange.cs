using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.Factory.Realizations.Layers;
using Pathfinding.GraphLib.Factory.Realizations.NeighborhoodFactories;
using Pathfinding.GraphLib.UnitTest.Realizations;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using Shared.Primitives.Single;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations
{
    using Assemble = GraphAssemble<TestVertex>;

    internal sealed class TestPathfindingRange : Singleton<TestPathfindingRange, IEnumerable<TestVertex>>, IEnumerable<TestVertex>
    {
        private readonly Lazy<IGraph<TestVertex>> testGraph;

        private IGraph<TestVertex> TestGraph => testGraph.Value;

        private TestPathfindingRange()
        {
            testGraph = new Lazy<IGraph<TestVertex>>(CreateGraph);
        }

        public IEnumerator<TestVertex> GetEnumerator()
        {
            yield return Get(4, 4);
            yield return Get(3, 13);
            yield return Get(8, 17);
            yield return Get(11, 13);
            yield return Get(12, 6);
            yield return Get(16, 2);
            yield return Get(21, 4);
            yield return Get(22, 9);
            yield return Get(23, 12);
            yield return Get(20, 16);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IGraph<TestVertex> CreateGraph()
        {
            var vertexFactory = new TestVertexFactory();
            var graphFactory = new GraphFactory<TestVertex>();
            var assemble = new Assemble(vertexFactory, graphFactory);
            var layers = new ILayer[]
            {
                new TestObstacleLayer(),
                new TestCostLayer(),
                new NeighborhoodLayer(new MooreNeighborhoodFactory())
            };
            return assemble.AssembleGraph(layers, Constants.Width, Constants.Length);
        }

        private TestVertex Get(int x, int y)
        {
            return TestGraph.Get(new Coordinate(x, y));
        }
    }
}
