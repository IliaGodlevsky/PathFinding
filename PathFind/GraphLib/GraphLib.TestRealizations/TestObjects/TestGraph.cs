using GraphLib.Base;

namespace GraphLib.TestRealizations
{
    public sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes)
            : base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
