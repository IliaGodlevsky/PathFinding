using Common;
using GraphLib.Interface;
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

        protected BaseVertexCost() : this(CostRange.GetRandomValueFromRange())
        {

        }

        static BaseVertexCost()
        {
            CostRange = new ValueRange(9, 1);
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

        public static ValueRange CostRange { get; set; }
    }
}
