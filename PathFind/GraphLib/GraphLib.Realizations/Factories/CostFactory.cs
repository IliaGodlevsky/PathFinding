using GraphLib.Interfaces;
using GraphLib.Interfaces.Factories;
using Random.Extensions;
using Random.Interface;

namespace GraphLib.Realizations.Factories
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public CostFactory(IRandom random)
        {
            
        }

        public IVertexCost CreateCost(int cost)
        {
            return new VertexCost(cost);
        }
    }
}