using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.GraphLib.Factory.Extensions;
using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.Factory.Realizations.GraphAssembles;
using Pathfinding.GraphLib.Factory.Realizations.GraphFactories;
using Pathfinding.GraphLib.UnitTest.Realizations;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories;
using Pathfinding.GraphLib.UnitTest.Realizations.TestFactories.Layers;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.AlgorithmLib.Core.Realizations.Tests.Realizations
{
    using Assemble = GraphAssemble<Graph2D<TestVertex>, TestVertex>;

    internal sealed class TestPathfindingRange : IEnumerable<TestVertex>
    {
        private readonly Lazy<Graph2D<TestVertex>> testGraph;

        private Graph2D<TestVertex> TestGraph => testGraph.Value;

        public TestPathfindingRange()
        {
            testGraph = new Lazy<Graph2D<TestVertex>>(CreateGraph);
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

        private Graph2D<TestVertex> CreateGraph()
        {
            var dimensions = Constants.DimensionSizes2D;
            var vertexFactory = new TestVertexFactory();
            var coordinateFactory = new TestCoordinateFactory();
            var graphFactory = new Graph2DFactory<TestVertex>();
            var assemble = new Assemble(vertexFactory, coordinateFactory, graphFactory);
            var layers = new ILayer<Graph2D<TestVertex>, TestVertex>[]
            {
                new ObstacleLayer(),
                new CostLayer(),
                new NeighborhoodLayer()
            };
            return assemble.AssembleGraph(layers, dimensions);
        }

        private TestVertex Get(int x, int y)
        {
            return TestGraph.Get(new TestCoordinate(x, y));
        }
    }
}
