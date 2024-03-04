using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Pathfinding.App.Console.Views
{
    internal sealed class View(IUnit model, IInput<int> input) : IDisplayable
    {
        private readonly IUnit unit = model;
        private readonly IInput<int> intInput = input;

        public void Display()
        {
            bool isClosureRequested = false;
            while (!isClosureRequested)
            {
                try
                {
                    AppLayout.SetCursorPositionUnderGraphField();
                    var menuItems = unit.GetMenuItems();
                    var menuItem = InputMenuItem(menuItems);
                    menuItem.Execute();
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