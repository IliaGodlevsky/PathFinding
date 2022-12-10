using Pathfinding.App.Console.Exceptions;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
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
                var options = GetMenuOptions();
                Screen.SetCursorPositionUnderMenu(1);
                try
                {
                    int index = InputItemIndex(options.Message, options.MenuRange);
                    var command = options.Items[index];
                    command.Execute();
                }
                catch (ExitRequiredException)
                {
                    isClosureRequested = true;
                }
            }
        }

        private (string Message, IReadOnlyList<IMenuItem> Items, InclusiveValueRange<int> MenuRange) GetMenuOptions()
        {
            var menuItems = unit.MenuItems.Where(item => item.CanBeExecuted()).ToReadOnly();
            var menuList = menuItems.CreateMenuList(unit.MenuItemColumns);
            var menuRange = new InclusiveValueRange<int>(menuItems.Count, 1);
            var message = menuList + "\n" + MessagesTexts.MenuOptionChoiceMsg;
            return (message, menuItems, menuRange);
        }

        private int InputItemIndex(string message, InclusiveValueRange<int> menuRange)
        {
            using (Cursor.CleanUpAfter())
            {
                return intInput.Input(message, menuRange) - 1;
            }
        }
    }
}