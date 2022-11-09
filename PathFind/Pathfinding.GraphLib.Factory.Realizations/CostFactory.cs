using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;
using Shared.Random.Extensions;

namespace Pathfinding.GraphLib.Factory.Realizations
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