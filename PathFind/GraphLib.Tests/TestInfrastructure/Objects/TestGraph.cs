using GraphLib.Base;

namespace GraphLib.Tests.TestInfrastructure.Objects
{
    internal class TestGraph : BaseGraph
    {
        public TestGraph(params int[] dimensionSizes) :
            base(dimensionSizes.Length, dimensionSizes)
        {

        }

        public override string GetFormattedData(string format)
        {
            return string.Empty;
        }
    }
}
