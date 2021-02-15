using Common;
using GraphLib.Interface;

namespace Algorithm.Realizations.Tests.Objects
{
    internal sealed class TestCost : IVertexCost
    {
        private static ValueRange CostRange { get; }

        public TestCost()
        {
            CurrentCost = CostRange.GetRandomValueFromRange();
        }

        static TestCost()
        {
            CostRange = new ValueRange(9, 1);
        }

        public int CurrentCost { get; }

        public object Clone()
        {
            return null;
        }
    }
}
