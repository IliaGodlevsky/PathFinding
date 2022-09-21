using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph
    {
        public TestGraph(IReadOnlyCollection<IVertex> vertices, IReadOnlyList<int> dimensionSizes)
            : base(dimensionSizes.Count, vertices, dimensionSizes)
        {

        }
    }
}
