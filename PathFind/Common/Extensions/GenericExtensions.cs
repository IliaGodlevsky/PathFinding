using Common.Attrbiutes;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static string GetDescription<T>(this T self)
        {
            return self.GetAttributeOrNull<DescriptionAttribute>()?.Description
                ?? DescriptionAttribute.Default.Description;
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

        public static IReadOnlyCollection<T> GetAttached<T>(this object self, params object[] ctorParams)
        {
            return self
                .GetType()
                .Assembly
                .GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type))
                .Where(type => type.IsAttachedTo(self))
                .Select(type => (T)Activator.CreateInstance(type, ctorParams))
                .OrderByOrderAttribute()
                .ToArray();
        }

        public static T As<T>(this object self, T @default = null) where T : class
        {
            return self as T ?? @default;
        }
    }
}
