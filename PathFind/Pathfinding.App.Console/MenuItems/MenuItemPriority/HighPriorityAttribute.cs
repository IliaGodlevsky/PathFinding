using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MenuItemPriority
{
    internal sealed class HighPriorityAttribute : OrderAttribute
    {
        public HighPriorityAttribute() 
            : base(int.MinValue / 2)
        {
        }
    }
}
