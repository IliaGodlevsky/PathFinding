using Common.Attrbiutes;
using System;

namespace ConsoleVersion.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MenuItemAttribute : OrderAttribute
    {
        public MenuItemAttribute(int order) : base(order)
        {

        }
    }
}