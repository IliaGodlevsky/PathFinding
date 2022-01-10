using Common.Attrbiutes;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Common.Extensions
{
    public static class GenericExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreater<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLess<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsGreaterOrEqual<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLessOrEqual<T>(this T first, T second)
            where T : IComparable
        {
            return first.CompareTo(second) <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetDescriptionAttributeValueOrEmpty<T>(this T self)
        {
            return self.GetAttributeOrNull<DescriptionAttribute>()?.Description ?? string.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetOrderAttributeValueOrMaxValue<T>(this T self)
        {
            return self.GetAttributeOrNull<OrderAttribute>()?.Order ?? int.MaxValue;
        }

        public static TAttribute GetAttributeOrNull<TAttribute>(this object self)
            where TAttribute : Attribute
        {
            var memberInfo = self is Enum e
                ? (MemberInfo)e.GetType().GetField(e.ToString())
                : (MemberInfo)self.GetType();
            return memberInfo.GetAttributeOrNull<TAttribute>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsOneOf<T>(this T self, params T[] objects)
        {
            return objects.Any(o => o.Equals(self));
        }
    }
}
