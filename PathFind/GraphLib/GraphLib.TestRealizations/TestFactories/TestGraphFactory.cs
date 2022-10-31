using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestGraphFactory : IGraphFactory<TestGraph, TestVertex>
    {
        public TestGraph CreateGraph(IReadOnlyCollection<TestVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new TestGraph(vertices, dimensionSizes);
        }
    }
}
