using Shared.Primitives.Attributes;

namespace Pathfinding.App.Console.MenuItems.MenuItemPriority
{
    internal sealed class MediumPriorityAttribute : OrderAttribute
    {
        public MediumPriorityAttribute() 
            : base(int.MinValue / 4)
        {
        }
    }
}
