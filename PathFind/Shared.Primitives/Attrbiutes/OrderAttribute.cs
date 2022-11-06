﻿using System;

namespace Shared.Primitives.Attrbiutes
{
    [AttributeUsage(AttributeTargets.All)]
    public class OrderAttribute : Attribute
    {
        public static readonly OrderAttribute Default = new OrderAttribute();

        public int Order { get; }

        public OrderAttribute(int order)
        {
            Order = order;
        }

        public OrderAttribute() : this(int.MaxValue)
        {

        }

        public override bool Equals(object obj)
        {
            return obj is OrderAttribute attribute
                && attribute.Order == Order;
        }

        public override int GetHashCode()
        {
            return Order.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return Equals(Default);
        }
    }
}
