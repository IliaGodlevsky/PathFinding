using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Views
{
    internal sealed class View : IDisplayable
    {
        private readonly IUnit unit;
        private IInput<int> intInput;

        public View(IUnit model, IInput<int> input)
        {
            this.intInput = input;
            this.unit = model;
        }

        public void Display()
        {
            bool isClosureRequested = false;
            while (!isClosureRequested)
            {
                var menuItems = unit.MenuItems;
                var options = GetMenuOptions(menuItems);
                Screen.SetCursorPositionUnderMenu(1);
                try
                {
                    int index = InputItemIndex(options);
                    var menuItem = menuItems[index];
                    menuItem.Execute();
                }
                catch (ExitRequiredException)
                {
                    isClosureRequested = true;
                }
            }
        }

        private (string Message, InclusiveValueRange<int> MenuRange) GetMenuOptions(IReadOnlyCollection<IAction> menuItems)
        {
            int columnsNumber = (int)Math.Ceiling(menuItems.Count / 4.0);
            var menuList = menuItems.CreateMenuList(columnsNumber);
            var menuRange = new InclusiveValueRange<int>(menuItems.Count, 1);
            var message = string.Concat(menuList, "\n", Languages.MenuOptionChoiceMsg);
            return (message, menuRange);
        }

        private int InputItemIndex((string Message, InclusiveValueRange<int> MenuRange) options)
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                return intInput.Input(options.Message, options.MenuRange) - 1;
            }
        }
    }
}