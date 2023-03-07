using Pathfinding.App.Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, int columnsNumber = 2)
        {
            return items.CreateMenuList(item => item.ToString(), columnsNumber);
        }

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, Func<T, string> descriptionSelector,
            int columnsNumber = 2)
        {
            return new(items.Select(descriptionSelector), columnsNumber);
        }
    }
}
