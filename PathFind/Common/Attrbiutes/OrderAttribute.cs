using System;

namespace Common.Attrbiutes
{
    [AttributeUsage(AttributeTargets.All)]
    public class OrderAttribute : Attribute
    {
        public int Order { get; }

        public OrderAttribute(int order)
        {
            Order = order;
        }
    }
}
