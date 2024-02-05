using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Shared.Extensions;
using Shared.Primitives.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal abstract class Unit : IUnit
    {
        private IReadOnlyCollection<IMenuItem> MenuItems { get; }

        protected virtual IMenuItem ExitMenuItem { get; }
            = new ExitMenuItem();

        protected Unit(IReadOnlyCollection<IMenuItem> menuItems)
        {
            MenuItems = menuItems;
        }

        public IReadOnlyList<IMenuItem> GetMenuItems()
        {
            return MenuItems
                .Where(CanBeExecuted)
                .Append(ExitMenuItem)
                .OrderByOrderAttribute()
                .ToReadOnly();
        }

        private static bool CanBeExecuted(IMenuItem item)
        {
            bool canBeExecuted = item is IConditionedMenuItem m
                && m.CanBeExecuted();
            return item is not IConditionedMenuItem || canBeExecuted;
        }
    }
}