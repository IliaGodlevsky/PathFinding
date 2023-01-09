using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MenuItemPriority
{
    internal sealed class LowPriorityAttribute : OrderAttribute
    {
        public LowPriorityAttribute() 
            : base(int.MinValue / 8)
        {
        }
    }
}
