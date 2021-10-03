using Common.Extensions;
using Common.Interface;
using Common.ValueRanges;
using GraphLib.Interfaces;
using System;

namespace GraphLib.Base
{
    [Serializable]
    public abstract class BaseVertexCost : IVertexCost, ICloneable<IVertexCost>
    {
        public static InclusiveValueRange<int> CostRange { get; set; }

        public int CurrentCost { get; protected set; }

        protected BaseVertexCost(int cost)
        {
            cost = CostRange.ReturnInRange(cost);
            CurrentCost = cost;
        }

        protected BaseVertexCost()
            : this(CostRange.GetRandomValue())
        {

        }

        static BaseVertexCost()
        {
            CostRange = new InclusiveValueRange<int>(9, 1);
        }

        public override bool Equals(object obj)
        {
            return obj is IVertexCost cost && cost.CurrentCost == CurrentCost;
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public abstract IVertexCost Clone();
    }
}
