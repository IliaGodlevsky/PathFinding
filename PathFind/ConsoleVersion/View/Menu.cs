using Common.Extensions;
using ConsoleVersion.Attributes;
using ConsoleVersion.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleVersion.ViewModel
{
    internal static class Menu
    {
        /// <summary>
        /// Creates a menu from <paramref name="menuItemsNames"/>
        /// splited by the <paramref name="columns"/> number
        /// </summary>
        /// <param name="menuItemsNames"></param>
        /// <param name="columns"></param>
        /// <returns>A string representing the menu, splited by 
        /// the number of <paramref name="columns"/> or an empty 
        /// string if <paramref name="menuItemsNames"/> array is empty</returns>
        /// <remarks>If <paramref name="columns"/> is less or equal than 
        /// 0 <paramref name="columns"/> will be set to 1</remarks>
        public static string CreateMenu(IEnumerable<string> menuItemsNames, int columns = 2)
        {
            if (!menuItemsNames.Any())
            {
                return string.Empty;
            }

            if (columns <= 0)
            {
                columns = 1;
            }

            StringBuilder menu = new StringBuilder("\n");

            int menuItemNumber = 0;
            int menuItemNumberPad = Convert.ToInt32(Math.Log10(menuItemsNames.Count())) + 1;
            int longestNameLength = menuItemsNames.Max(str => str.Length) + 1;

            foreach (var name in menuItemsNames)
            {
                string paddedName = name.PadRight(longestNameLength);
                string separator = CreateSeparator(menuItemNumber, columns);
                string stringedNenuItemNumber = (++menuItemNumber).ToString();
                string paddedMenuItemNumber = stringedNenuItemNumber.PadLeft(menuItemNumberPad);
                menu.AppendFormat(Resources.MenuFormat + separator, paddedMenuItemNumber, paddedName);
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
                    var header = GetMenuItemHeader(method);
                    menuActions.Add(header, action);
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
            return attribute.Priority.GetValue<int>();
        }

        private static string GetMenuItemHeader(MethodInfo method)
        {
            var attribute = method.GetAttribute<MenuItemAttribute>();
            return attribute.Header;
        }

        private static string CreateSeparator(int currentMenuItemNumber, int columns)
        {
            return (currentMenuItemNumber + 1) % columns == 0 ? "\n" : " ";
        }
    }
}
