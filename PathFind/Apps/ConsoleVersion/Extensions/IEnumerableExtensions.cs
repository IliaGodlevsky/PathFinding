using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleVersion.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static void OnEndPointChosen(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.OnEndPointChosen());
        }

        public static void OnMarkedToReplaceIntermediate(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.OnMarkedToReplaceIntermediate());
        }

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, Func<T, string> itemName, int columnsNumber = 2)
        {
            return new MenuList(items.Select(itemName), columnsNumber);
        }

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, int columnsNumber = 2)
        {
            return items.CreateMenuList(item => item.ToString(), columnsNumber);
        }
    }
}
