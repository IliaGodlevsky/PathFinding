using Common.Interface;
using GraphLib.Base;
using GraphLib.Extensions;
using GraphLib.Interfaces;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph, IGraph, ICloneable<IGraph>
    {
        public TestGraph(params int[] dimensionSizes)
            : base(dimensionSizes.Length, dimensionSizes)
        {

        }

        public override IGraph Clone()
        {
            var graph = new TestGraph(DimensionsSizes);
            return graph.CopyVerticesDeeply(this);
        }
    }
}
