using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        private readonly IRandom random;

        public CostFactory(IRandom random)
        {
            this.random = random;
        }

        public IVertexCost CreateCost(int cost)
        {
            return new VertexCost(cost);
        }

        public IVertexCost CreateCost()
        {
            int randomCost = random.NextInt(VertexCost.CostRange);
            return new VertexCost(randomCost);
        }
    }
}