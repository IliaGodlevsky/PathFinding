using GraphLib.Base;

namespace GraphLib.Serialization.Tests.Objects
{
    internal class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }
    }
}
