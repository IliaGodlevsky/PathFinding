using Pathfinding.GraphLib.Core.Interface;
using Shared.Primitives.ValueRange;
using Shared.Primitives.ValueRange.Extensions;

namespace Pathfinding.GraphLib.Core.Realizations
{
    public class VertexCost : IVertexCost
    {
        public static InclusiveValueRange<int> CostRange { get; set; }

        static VertexCost()
        {
            CostRange = new InclusiveValueRange<int>(9, 1);
        }

        public int CurrentCost { get; protected set; }

        protected VertexCost(int cost)
        {
            CurrentCost = CostRange.ReturnInRange(cost);
        }

        public override bool Equals(object obj)
        {
            return obj is IVertexCost cost && cost.CurrentCost == CurrentCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }
    }
}
