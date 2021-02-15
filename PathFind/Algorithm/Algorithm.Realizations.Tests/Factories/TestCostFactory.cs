using Algorithm.Realizations.Tests.Objects;
using GraphLib.Interface;

namespace Algorithm.Realizations.Tests.Factories
{
    internal class TestCostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new TestCost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return CreateCost();
        }
    }
}
