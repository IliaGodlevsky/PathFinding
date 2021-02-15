using GraphLib.Base;

namespace Algorithm.Realizations.Tests.Objects
{
    internal sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
