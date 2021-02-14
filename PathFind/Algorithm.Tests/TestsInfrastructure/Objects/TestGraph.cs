using GraphLib.Base;

namespace Algorithm.Tests.TestsInfrastructure.Objects
{
    internal sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
