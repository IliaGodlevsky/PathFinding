using GraphLib.Base;
using GraphLib.TestRealizations.TestObjects;
using System.Collections.Generic;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : Graph<TestVertex>
    {
        public TestGraph(IReadOnlyCollection<TestVertex> vertices, IReadOnlyList<int> dimensionSizes)
            : base(dimensionSizes.Count, vertices, dimensionSizes)
        {

        }
    }
}
