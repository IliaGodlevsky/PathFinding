using GraphLib.Interface;
using GraphLib.Realizations.Tests.Objects;

namespace GraphLib.Realizations.Tests.Factories
{
    internal class TestCostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new TestCost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new TestCost(cost);
        }
    }
}
