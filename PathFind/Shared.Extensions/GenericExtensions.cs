using System;
using System.Linq;
using System.Reflection;

namespace Shared.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(o => o.Equals(self));
        }
    }
}
