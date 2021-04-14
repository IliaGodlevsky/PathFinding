using GraphLib.Base;

namespace GraphLib.Realizations.Tests.Objects
{
    internal class TestCost : BaseVertexCost
    {
        public TestCost(int cost = 1) : base(cost)
        {

        }

        public TestCost() : base()
        {

        }
    }
}
