using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static IDisplayable CreateMenuList<T>(this IEnumerable<T> items, int columnsNumber = 2)
        {
            return new MenuList(items.Select(item => item.ToString()), columnsNumber);
        }
    }
}
