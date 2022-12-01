using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public class VertexCost : IVertexCost
    {
        private readonly string stringRepresentation;

        public int CurrentCost { get; }

        public InclusiveValueRange<int> CostRange { get; }

        public VertexCost(int cost, InclusiveValueRange<int> costRange)
        {
            CostRange = costRange;
            CurrentCost = cost;
            stringRepresentation = CurrentCost.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is IVertexCost cost && cost.CurrentCost == CurrentCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public override sealed string ToString()
        {
            return stringRepresentation;
        }

        public IVertexCost SetCost(int cost)
        {
            return new VertexCost(cost, CostRange);
        }
    }
}
