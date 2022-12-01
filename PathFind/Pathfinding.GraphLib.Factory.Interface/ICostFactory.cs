using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Factory.Interface
{
    public interface IVertexCostFactory
    {
        IVertexCost CreateCost(int cost, InclusiveValueRange<int> range);
    }
}
