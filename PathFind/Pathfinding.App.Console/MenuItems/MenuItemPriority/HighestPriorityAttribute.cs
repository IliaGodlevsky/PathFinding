using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MenuItemPriority
{
    internal sealed class HighestPriorityAttribute : OrderAttribute
    {
        public HighestPriorityAttribute() 
            : base(int.MinValue)
        {
        }
    }
}
