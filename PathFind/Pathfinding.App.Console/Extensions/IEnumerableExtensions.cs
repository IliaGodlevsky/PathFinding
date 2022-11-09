using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static void IncludeInRange(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.IncludeInRange());
        }

        public static void MarkAsIntermediateToReplace(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.MarkAsIntermediateToReplace());
        }

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, int columnsNumber = 2)
        {
            return new MenuList(items.Select(item => item.ToString()), columnsNumber);
        }
    }
}
