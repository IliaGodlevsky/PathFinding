﻿using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.MenuItems;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Units
{
    internal abstract class Unit : IUnit
    {
        private readonly Lazy<IReadOnlyCollection<IMenuItem>> menuItems;

        public IReadOnlyCollection<IMenuItem> MenuItems => menuItems.Value;

        protected Unit(IReadOnlyCollection<IMenuItem> menuItems)
        {
            this.menuItems = new(() => SetItems(menuItems));
        }

        protected IReadOnlyCollection<IMenuItem> SetItems(IReadOnlyCollection<IMenuItem> menuItems)
        {
            return menuItems
                .Append(GetExitMenuItem())
                .OrderBy(item => item.Order)
                .ToReadOnly();
        }

        protected virtual IMenuItem GetExitMenuItem()
        {
            return new ExitMenuItem();
        }
    }
}