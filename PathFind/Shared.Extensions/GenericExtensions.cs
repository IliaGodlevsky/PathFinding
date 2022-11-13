using System;
using System.Linq;

namespace Shared.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(obj => self.Equals(obj));
        }

        public static bool IsOneOf<T>(this T self, params IEquatable<T>[] objects)
        {
            return objects.Any(obj => obj.Equals(self));
        }
    }
}