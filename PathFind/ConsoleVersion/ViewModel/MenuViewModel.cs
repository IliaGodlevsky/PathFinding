using Common.Extensions;
using ConsoleVersion.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleVersion.ViewModel
{
    internal static class MenuViewModel
    {
        /// <summary>
        /// Creates a menu from <paramref name="menuItemsNames"/>
        /// splited by the <paramref name="columns"/> number
        /// </summary>
        /// <param name="menuItemsNames"></param>
        /// <param name="columns"></param>
        /// <returns>A string representing the menu, splited by 
        /// the number of <paramref name="columns"/></returns>
        public static string CreateMenu(IEnumerable<string> menuItemsNames, int columns = 2)
        {
            if (!menuItemsNames.Any())
            {
                return string.Empty;
            }

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
                string paddedMenuItemNumber = stringedNenuItemNumber.PadLeft(menuItemNumberPad);
                menu.AppendFormat(format + separator, paddedMenuItemNumber, paddedName);
            }

            return menu.ToString();
        }

        /// <summary>
        /// Returns a dictionary of delegates where as a key is 
        /// a method menu item name and as value is <typeparamref name="TAction"/>
        /// </summary>
        /// <typeparam name="TAction"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks>To get into the dictionary, a method must be marked with <see cref="MenuItemAttribute"/></remarks>
        public static Dictionary<string, TAction> GetMenuMethodsAsDelegates<TAction>(object target)
            where TAction : Delegate
        {
            var menuActions = new Dictionary<string, TAction>();

            foreach (var method in GetMenuMethods(target))
            {
                if (method.TryCreateDelegate(target, out TAction action))
                {
                    string menuItemName = GetMenuItemName(method);
                    menuActions.Add(menuItemName, action);
                }
            }

            return menuActions;
        }

        /// <summary>
        /// Returns an array of <see cref="MethodInfo"/> that 
        /// are marked with <see cref="MenuItemAttribute"/>
        /// </summary>
        /// <returns>An array of <see cref="MethodInfo"/></returns>
        private static IEnumerable<MethodInfo> GetMenuMethods(object target)
        {
            return target.GetType()
                .GetMethods()
                .Where(IsMenuMethod)
                .OrderBy(GetMenuItemPriority);
        }

        private static bool IsMenuMethod(MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(MenuItemAttribute));
        }

        private static int GetMenuItemPriority(MethodInfo method)
        {
            var attribute = method.GetAttribute<MenuItemAttribute>();
            return attribute.MenuItemPriority.GetValue<int>();
        }

        private static string GetMenuItemName(MethodInfo method)
        {
            var attribute = method.GetAttribute<MenuItemAttribute>();
            return attribute.MenuItemName;
        }
    }
}
