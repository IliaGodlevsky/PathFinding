using GraphLib.Interface;
using GraphLib.Tests.TestInfrastructure.Objects;

namespace GraphLib.Tests.TestInfrastructure.Factories
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
