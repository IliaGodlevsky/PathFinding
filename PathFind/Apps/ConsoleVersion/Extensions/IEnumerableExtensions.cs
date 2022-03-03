using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static void DisplayAll(this IEnumerable<IDisplayable> collection)
        {
            collection.ForEach(display => display.Display());
        }

        public static void MarkAsEndPoints(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.OnEndPointChosen());
        }

        public static void MarkToReplace(this IEnumerable<Vertex> vertices)
        {
            vertices.ForEach(vertex => vertex.OnMarkedToReplaceIntermediate());
        }
    }
}
