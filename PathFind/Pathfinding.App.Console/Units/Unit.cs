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

        private IReadOnlyCollection<IConditionedMenuItem> ConditionedMenuItems { get; }

        protected virtual ExitMenuItem ExitMenuItem { get; } = new ExitMenuItem();

        protected Unit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
        {
            MenuItems = menuItems;
            ConditionedMenuItems = conditioned;
        }

        public IReadOnlyList<IMenuItem> GetMenuItems()
        {
            return ConditionedMenuItems
                .Where(item => item.CanBeExecuted())
                .Concat(MenuItems)
                .Append(ExitMenuItem)
                .OrderByOrderAttribute()
                .ToReadOnly();
        }
    }
}