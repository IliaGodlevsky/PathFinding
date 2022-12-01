using Pathfinding.GraphLib.Factory.Interface;
using Pathfinding.GraphLib.UnitTest.Realizations.TestObjects;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestFactories
{
    public class TestGraphFactory : IGraphFactory<TestGraph, TestVertex>
    {
        public TestGraph CreateGraph(IReadOnlyCollection<TestVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new TestGraph(vertices, dimensionSizes);
        }
    }
}
