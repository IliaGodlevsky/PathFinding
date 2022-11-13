using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.Extensions;
using Shared.Primitives.ValueRange;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public class VertexCost : IVertexCost
    {
        public static InclusiveValueRange<int> CostRange { get; set; }

        private readonly string stringRepresentation;

        static VertexCost()
        {
            CostRange = new InclusiveValueRange<int>(9, 1);
        }

        public int CurrentCost { get; }

        public VertexCost(int cost)
        {
            CurrentCost = CostRange.ReturnInRange(cost);
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
    }
}
