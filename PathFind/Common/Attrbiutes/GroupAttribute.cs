using System;

namespace Common.Attrbiutes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public abstract class GroupAttribute : Attribute
    {
        public int OrderInGroup { get; }

        public GroupAttribute(int order)
        {
            OrderInGroup = order;
        }
    }
}
