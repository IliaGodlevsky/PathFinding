using GraphLib.Base;

namespace GraphLib.Realizations.Tests.Objects
{
    internal class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
