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
            StringBuilder menu = new StringBuilder("\n");

            int menuItemNumber = 0;
            int menuItemNumberPad = (int)Math.Log10(menuItemsNames.Count()) + 1;
            int longestNameLength = menuItemsNames.Max(str => str.Length) + 1;
            string format = ConsoleVersionResources.MenuFormat;

            foreach (var name in menuItemsNames)
            {
                string paddedName = name.PadRight(longestNameLength);
                string separator = (menuItemNumber + 1) % columns == 0 ? "\n" : " ";
                string stringedNenuItemNumber = (++menuItemNumber).ToString();
                var paddedMenuItemNumber = stringedNenuItemNumber.PadLeft(menuItemNumberPad);
                menu.AppendFormat(format + separator, paddedMenuItemNumber, paddedName);
            }

            return menu.ToString();
        }

        public Dictionary<string, TAction> GetMenuActions<TAction>()
            where TAction : Delegate
        {
            var menuActions = new Dictionary<string, TAction>();

            foreach (var method in GetMenuMethods())
            {
                if (method.TryCreateDelegate(mainModel, out TAction action))
                {
                    string menuItemName = GetMenuItemName(method);
                    menuActions.Add(menuItemName, action);
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
