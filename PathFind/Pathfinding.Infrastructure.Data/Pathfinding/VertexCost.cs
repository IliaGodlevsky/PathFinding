using Pathfinding.Domain.Interface;
using Pathfinding.Shared.Primitives;

namespace Pathfinding.Infrastructure.Data.Pathfinding
{
    public sealed class VertexCost : IVertexCost
    {
        public InclusiveValueRange<int> CostRange { get; set; }

        public int CurrentCost { get; set; }

        public VertexCost(int cost, InclusiveValueRange<int> costRange)
        {
            CostRange = costRange;
            CurrentCost = cost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }
    }
}
