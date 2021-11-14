using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using GraphLib.Realizations.VertexCost;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost(int cost)
        {
            return new WeightableVertexCost(cost);
        }
    }
}
