using GraphLib.Interface;
using GraphLib.Realizations.VertexCost;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new WeightableVertexCost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new WeightableVertexCost(cost);
        }
    }
}
