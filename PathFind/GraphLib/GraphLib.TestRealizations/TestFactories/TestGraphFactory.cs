using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using System.Collections.Generic;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestGraphFactory : IGraphFactory
    {
        public IGraph CreateGraph(IReadOnlyCollection<IVertex> vertices, IReadOnlyList<int> dimensionSizes)
        {
            return new TestGraph(vertices, dimensionSizes);
        }
    }
}
