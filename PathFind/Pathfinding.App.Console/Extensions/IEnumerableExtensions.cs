using Pathfinding.App.Console.Model;
using Pathfinding.App.Console.Model.Notes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static MenuList CreateMenuList<T>(this IEnumerable<T> items,
            int columnsNumber = 2)
        {
            return items.CreateMenuList(item => item.ToString(), columnsNumber, string.Empty);
        }

        public static MenuList CreateTable<T>(this IEnumerable<T> items, string header)
        {
            return items.CreateMenuList(item => item.ToString(), 1, header);
        }

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, Func<T, string> descriptionSelector,
            int columnsNumber = 2, string header = null)
        {
            return new(items.Select(descriptionSelector), columnsNumber, header);
        }

        public static Table<T> ToTable<T>(this IEnumerable<T> items)
        {
            return new Table<T>(items);
        }
    }
}
