using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal abstract class Unit : IUnit
    {
        private IReadOnlyCollection<IMenuItem> MenuItems { get; }

        protected Unit(IReadOnlyCollection<IMenuItem> menuItems)
        {
            MenuItems = menuItems;
        }

        public IReadOnlyList<IMenuItem> GetMenuItems()
        {
            return MenuItems
                .Where(CanBeExecuted)
                .OrderByOrderAttribute()
                .ToList()
                .AsReadOnly();
        }

        private static bool CanBeExecuted(IMenuItem item)
        {
            bool canBeExecuted = item is IConditionedMenuItem c && c.CanBeExecuted();
            return item is not IConditionedMenuItem || canBeExecuted;
        }
    }
}