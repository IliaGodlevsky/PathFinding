using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinding.App.Console.Views
{
    internal sealed class View(IUnit model, IInput<int> input)
    {
        private readonly IUnit unit = model;
        private readonly IInput<int> intInput = input;

        public async Task DisplayAsync(CancellationToken token = default)
        {
            bool isClosureRequested = false;
            while (!isClosureRequested)
            {
                if (token.IsCancellationRequested) break;
                try
                {
                    AppLayout.SetCursorPositionUnderGraphField();
                    var menuItems = unit.GetMenuItems();
                    var menuItem = InputMenuItem(menuItems);
                    await menuItem.ExecuteAsync(token);
                }
                catch (ExitRequiredException)
                {
                    isClosureRequested = true;
                }
            }
        }

        private IMenuItem InputMenuItem(IReadOnlyList<IMenuItem> menuItems)
        {
            int columnsNumber = (int)Math.Ceiling(menuItems.Count / 4.0);
            var menuList = menuItems.CreateMenuList(columnsNumber);
            var menuRange = new InclusiveValueRange<int>(menuItems.Count, 1);
            string menu = string.Concat(menuList, "\n", Languages.MenuOptionChoiceMsg);
            using (Cursor.UseCurrentPositionWithClean())
            {
                int index = intInput.Input(menu, menuRange) - 1;
                var menuItem = menuItems[index];
                return menuItem;
            }
        }
    }
}