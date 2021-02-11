using GraphLib.Interface;
using GraphLib.VertexCost;

namespace GraphLib.Factories
{
    public class CostFactory : IVertexCostFactory
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
