using Pathfinding.GraphLib.Core.Interface;
using Pathfinding.GraphLib.Core.Realizations;
using Pathfinding.GraphLib.Factory.Interface;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Factory.Realizations
{
    public sealed class CostFactory : IVertexCostFactory
    {
        public IVertexCost CreateCost(int cost, InclusiveValueRange<int> range)
        {
            return new VertexCost(cost, range);
        }
    }
}