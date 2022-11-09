using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class MenuItemAttribute : OrderAttribute
    {
        public string Header { get; }

        public MenuItemAttribute(string header, int order) : base(order)
        {
            Header = header;
        }
    }
}