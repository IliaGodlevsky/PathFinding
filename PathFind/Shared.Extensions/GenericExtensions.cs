using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(obj => self.Equals(obj));
        }

        public static IEnumerable<T> Enumerate<T>(this T obj)
        {
            yield return obj;
        }
    }
}