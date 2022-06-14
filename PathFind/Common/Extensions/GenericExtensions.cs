using Common.Attrbiutes;
using System;
using System.Linq;
using System.Reflection;

namespace Common.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsGreater<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) > 0;
        }

        public static bool IsLess<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) < 0;
        }

        public static bool IsGreaterOrEqual<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool IsLessOrEqual<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) <= 0;
        }

        public static bool IsBetween<T>(this T value, T upper, T lower)
            where T : IComparable
        {
            return value.IsLessOrEqual(upper) && value.IsGreaterOrEqual(lower);
        }

        public static int GetOrder<T>(this T self)
        {
            return self.GetAttributeOrNull<OrderAttribute>()?.Order
                ?? OrderAttribute.Default.Order;
        }

        public static TAttribute GetAttributeOrNull<TAttribute>(this object self)
            where TAttribute : Attribute
        {
            var memberInfo = self is Enum e
                ? (MemberInfo)e.GetType().GetField(e.ToString())
                : (MemberInfo)self.GetType();
            return memberInfo.GetAttributeOrNull<TAttribute>();
        }

        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(o => o.Equals(self));
        }

        public static T As<T>(this object self, T @default = null) where T : class
        {
            return self as T ?? @default;
        }
    }
}
