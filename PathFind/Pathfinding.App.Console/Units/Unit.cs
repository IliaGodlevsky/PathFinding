using Pathfinding.App.Console.Interface;
using Shared.Primitives.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal abstract class Unit : IUnit
    {
        private IReadOnlyCollection<IConditionedMenuItem> ConditionedMenuItems { get; }

        private IReadOnlyCollection<IMenuItem> MenuItems { get; }

        protected Unit(IReadOnlyCollection<IMenuItem> menuItems,
            IReadOnlyCollection<IConditionedMenuItem> conditioned)
        {
            MenuItems = menuItems;
            ConditionedMenuItems = conditioned;
        }

        public IReadOnlyList<IMenuItem> GetMenuItems()
        {
            var items = ConditionedMenuItems
                .Where(item => item.CanBeExecuted())
                .Concat(MenuItems)
                .OrderByOrderAttribute()
                .ToArray();
            return Array.AsReadOnly(items);
        }
    }
}