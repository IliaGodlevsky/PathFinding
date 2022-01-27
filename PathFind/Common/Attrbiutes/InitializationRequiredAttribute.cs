using System;

namespace Common.Attrbiutes
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InitializationRequiredAttribute : OrderAttribute
    {
        public InitializationRequiredAttribute(int order) : base(order)
        {
        }
    }
}
