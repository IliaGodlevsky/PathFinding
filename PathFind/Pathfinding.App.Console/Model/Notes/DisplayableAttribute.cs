using Shared.Primitives.Attributes;
using System;

namespace Pathfinding.App.Console.Model.Notes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    internal sealed class DisplayableAttribute : OrderAttribute
    {
        public DisplayableAttribute(int order)
            : base(order)
        {
        }
    }
}
