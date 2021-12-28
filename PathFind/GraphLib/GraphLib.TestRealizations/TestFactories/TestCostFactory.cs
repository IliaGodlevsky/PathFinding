using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.TestRealizations.TestObjects;
using Random.Extensions;
using Random.Interface;
using Random.Realizations.Generators;

namespace GraphLib.TestRealizations.TestFactories
{
    public class TestCostFactory : IVertexCostFactory
    {
        public TestCostFactory()
        {
            random = new PseudoRandom();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new TestVertexCost(cost);
        }

        public IVertexCost CreateCost()
        {
            int randomCost = random.Next(TestVertexCost.CostRange);
            return new TestVertexCost(randomCost);
        }

        private readonly IRandom random;
    }
}
