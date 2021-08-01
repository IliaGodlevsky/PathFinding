﻿using Common.Extensions;
using Common.ValueRanges;
using GraphLib.Interfaces;
using System;

namespace GraphLib.Base
{
    [Serializable]
    public abstract class BaseVertexCost : IVertexCost
    {
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

        public abstract IVertexCost Clone();

        public static InclusiveValueRange<int> CostRange { get; set; }
    }
}
