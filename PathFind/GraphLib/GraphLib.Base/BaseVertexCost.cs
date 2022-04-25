using GraphLib.Interfaces;
using ValueRange;
using ValueRange.Extensions;

namespace GraphLib.Base
{
    public abstract class BaseVertexCost : IVertexCost
    {
        public static InclusiveValueRange<int> CostRange { get; set; }

        static BaseVertexCost()
        {
            CostRange = new InclusiveValueRange<int>(9, 1);
        }

        public int CurrentCost { get; protected set; }

        protected BaseVertexCost(int cost)
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
