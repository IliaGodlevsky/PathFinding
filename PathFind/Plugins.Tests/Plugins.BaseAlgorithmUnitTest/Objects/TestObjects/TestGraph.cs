using GraphLib.Base;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes)
            : base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
