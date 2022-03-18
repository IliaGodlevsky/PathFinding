using GraphLib.Base;
using GraphLib.Interfaces;
using System.Collections.Generic;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph
    {
        public TestGraph(IEnumerable<IVertex> vertices, params int[] dimensionSizes)
            : base(dimensionSizes.Length, vertices, dimensionSizes)
        {

        }
    }
}
