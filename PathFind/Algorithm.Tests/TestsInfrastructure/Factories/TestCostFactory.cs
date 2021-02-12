using Algorithm.Tests.TestsInfrastructure.Objects;
using GraphLib.Interface;

namespace Algorithm.Tests.TestsInfrastructure.Factories
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
