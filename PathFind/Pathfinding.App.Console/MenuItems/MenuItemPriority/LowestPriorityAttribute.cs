using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MenuItemPriority
{
    internal sealed class LowestPriorityAttribute : OrderAttribute
    {
        public LowestPriorityAttribute()
            : base(int.MaxValue)
        {
        }
    }
}
