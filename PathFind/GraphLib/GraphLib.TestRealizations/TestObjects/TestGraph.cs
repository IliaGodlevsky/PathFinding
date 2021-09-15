using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes)
            : base(dimensionSizes.Length, dimensionSizes)
        {

        }

        public override IGraph Clone()
        {
            var graph = new TestGraph(DimensionsSizes);
            return graph.CloneVertices(this);
        }
    }
}
