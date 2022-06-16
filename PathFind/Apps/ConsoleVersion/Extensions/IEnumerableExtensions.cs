using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Model;
using System.Collections.Generic;

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

        public static MenuList ToMenuList(this IEnumerable<string> strings, int columnsNumber = 2)
        {
            return new MenuList(strings, columnsNumber);
        }
    }
}
