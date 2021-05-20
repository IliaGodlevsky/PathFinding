using GraphLib.Base;

namespace GraphLib.Extensions.Tests.TestObjects
{
    internal sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
