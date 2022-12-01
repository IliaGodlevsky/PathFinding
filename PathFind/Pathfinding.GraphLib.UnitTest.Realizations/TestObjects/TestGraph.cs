using Pathfinding.GraphLib.Core.Abstractions;
using System.Collections.Generic;

namespace Pathfinding.GraphLib.UnitTest.Realizations.TestObjects
{
    public sealed class TestGraph : Graph<TestVertex>
    {
        public TestGraph(IReadOnlyCollection<TestVertex> vertices, IReadOnlyList<int> dimensionSizes)
            : base(dimensionSizes.Count, vertices, dimensionSizes)
        {

        }
    }
}
