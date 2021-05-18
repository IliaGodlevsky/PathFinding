using GraphLib.Base;
using System.Linq;

namespace Plugins.BaseAlgorithmUnitTest.Objects.TestObjects
{
    internal sealed class TestGraph : BaseGraph
    {
        public int Width => DimensionsSizes.First();

        public int Length => DimensionsSizes.Last();

        public TestGraph(params int[] dimensionSizes)
            : base(numberOfDimensions: 2, dimensionSizes)
        {

        }
    }
}
