using Common.Extensions.EnumerableExtensions;
using ConsoleVersion.Interface;
using System.Collections.Generic;

namespace ConsoleVersion.Extensions
{
    internal static class IEnumerableExtensions
    {
        public static void DisplayAll(this IEnumerable<IDisplayable> collection)
        {
            collection.ForEach(display => display.Display());
        }
    }
}
