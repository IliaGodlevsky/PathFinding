using Common.Attrbiutes;
using System;
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

        /// <summary>
        /// Returns <see cref="DescriptionAttribute"/>'s value of type name
        /// if <paramref name="self"/> is marked with this attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns>A <see cref="DescriptionAttribute"'s value or type name 
        /// if <paramref name="self"/> is not marked with this attribute</returns>
        /// <remarks>Type name for <see cref="Enum"/> is a field name</remarks>
        public static string GetDescriptionAttributeValueOrTypeName<T>(this T self)
        {
            MemberInfo type = self.GetType();
            if (self is Enum e)
            {
                type = e.GetType().GetField(e.ToString());
            }
            return type?.GetAttributeOrNull<DescriptionAttribute>()
                ?.Description ?? type.Name;
        }

        public static int GetGroupAttributeValueOrMaxValue<T>(this T self)
        {
            return self.GetAttributeOrNull<T, GroupAttribute>()?.OrderInGroup ?? int.MaxValue;
        }

        public static TAttribute GetAttributeOrNull<T, TAttribute>(this T self)
            where TAttribute : Attribute
        {
            MemberInfo type = self.GetType();
            if (self is Enum e)
            {
                type = e.GetType().GetField(e.ToString());
            }
            return type?.GetAttributeOrNull<TAttribute>();
        }

        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(o => o.Equals(self));
        }
    }
}
