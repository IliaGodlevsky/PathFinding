using Common.ValueRanges;
using GraphLib.Interfaces;
using System;

namespace GraphLib.Base
{
    [Serializable]
    public abstract class BaseVertexCost : IVertexCost
    {
        public virtual int CurrentCost { get; protected set; }

        protected BaseVertexCost(int cost)
        {
            cost = CostRange.ReturnInRange(cost);
            CurrentCost = cost;
        }

        protected BaseVertexCost()
        {
            CurrentCost = Random.Next(CostRange.LowerValueOfRange, CostRange.UpperValueOfRange + 1);
        }

        static BaseVertexCost()
        {
            CostRange = new InclusiveValueRange<int>(9, 1);
            Random = new Random();
        }

        public override bool Equals(object obj)
        {
            if (obj is IVertexCost cost)
            {
                return cost.CurrentCost == CurrentCost;
            }

            var message = "An error was occurred while comparing\n";
            message += $"an instance of {GetType().Name} and {obj?.GetType().Name}\n";
            throw new ArgumentException(message, nameof(obj));
        }

        public override int GetHashCode()
        {
            return CurrentCost.GetHashCode();
        }

        public abstract object Clone();

        public static InclusiveValueRange<int> CostRange { get; set; }
        private readonly static Random Random;
    }
}
