using System;

namespace Common.Attrbiutes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public abstract class GroupAttribute : OrderAttribute
    {
        public GroupAttribute(int order) : base(order)
        {

        }
    }
}
