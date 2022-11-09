using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
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

        public static MenuList CreateMenuList<T>(this IEnumerable<T> items, int columnsNumber = 2)
        {
            return new MenuList(items.Select(item => item.ToString()), columnsNumber);
        }
    }
}
