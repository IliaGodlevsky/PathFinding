using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Random;
using Shared.Random.Extensions;

namespace Pathfinding.App.WPF._3D.Model
{
    internal sealed class Vertex3DCostFactory : IVertexCostFactory
    {
        private readonly IRandom random;

        public Vertex3DCostFactory(IRandom random)
        {
            this.random = random;
        }

        public IVertexCost CreateCost(int cost)
        {
            return new Vertex3DCost(cost);
        }

        public IVertexCost CreateCost()
        {
            int randomCost = random.NextInt(Vertex3DCost.CostRange);
            return new Vertex3DCost(randomCost);
        }
    }
}
