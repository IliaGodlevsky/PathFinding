using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public sealed record class VertexCost : IVertexCost
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
