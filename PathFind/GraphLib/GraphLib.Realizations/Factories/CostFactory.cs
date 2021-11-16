using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.VertexCost;
using Random.Extensions;
using Random.Interface;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public CostFactory(IRandom random)
        {
            this.random = random;
        }

        public IVertexCost CreateCost(int cost)
        {
            return new WeightableVertexCost(cost);
        }

        public IVertexCost CreateCost()
        {
            int randomCost = random.Next(WeightableVertexCost.CostRange);
            return new WeightableVertexCost(randomCost);
        }

        private readonly IRandom random;
    }
}
