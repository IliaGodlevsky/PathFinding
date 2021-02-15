using GraphLib.Interface;
using GraphLib.Realizations.VertexCost;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost()
        {
            return new Cost();
        }

        public IVertexCost CreateCost(int cost)
        {
            return new Cost(cost);
        }
    }
}
