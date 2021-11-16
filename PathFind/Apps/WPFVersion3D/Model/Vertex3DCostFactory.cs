using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;

namespace WPFVersion3D.Model
{
    internal sealed class Vertex3DCostFactory : IVertexCostFactory
    {
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
            int randomCost = random.Next(Vertex3DCost.CostRange);
            return new Vertex3DCost(randomCost);
        }

        private readonly IRandom random;
    }
}
