using Common.Extensions;
using ConsoleVersion.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleVersion.ViewModel
{
    internal class MenuViewModel
    {
        public MenuViewModel(MainViewModel mainViewModel)
        {
            mainModel = mainViewModel;
        }

        public static string CreateMenu(IEnumerable<string> menuItemsNames, int columns = 2)
        {
            var menu = new StringBuilder("\n");

            int menuItemNumber = 0;
            var longestNameLength = menuItemsNames.Max(str => str.Length) + 1;
            var format = ConsoleVersionResources.MenuFormat;

            foreach (var name in menuItemsNames)
            {
                var paddedName = name.PadRight(longestNameLength);
                var separator = (menuItemNumber + 1) % columns == 0 ? "\n" : " ";
                menu.AppendFormat(format + separator, ++menuItemNumber, paddedName);
            }

            return menu.ToString();
        }

        public Dictionary<string, Action> GetMenuActions()
        {
            var menuActions = new Dictionary<string, Action>();

            foreach (var method in GetMenuMethods())
            {
                if (method.TryCreateDelegate(mainModel, out Action action))
                {
                    var description = GetMenuItemName(method);
                    menuActions.Add(description, action);
                }
            }

            return menuActions;
        }

        private IEnumerable<MethodInfo> GetMenuMethods()
        {
            return typeof(MainViewModel)
                .GetMethods()
                .Where(IsMenuMethod)
                .OrderBy(GetMenuItemPriority);
        }

        private bool IsMenuMethod(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private int GetMenuItemPriority(MethodInfo method)
        {
            return method.GetAttribute<MenuItemAttribute>().MenuItemPriority.GetValue<int>();
        }

        private string GetMenuItemName(MethodInfo method)
        {
            var attribute = method.GetAttribute<MenuItemAttribute>();
            return attribute.MenuItemName;
        }

        private readonly MainViewModel mainModel;
    }
}
