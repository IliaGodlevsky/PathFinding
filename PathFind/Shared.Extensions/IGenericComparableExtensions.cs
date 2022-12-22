using System;

namespace Shared.Extensions
{
    public static class IGenericComparableExtensions
    {
        public static bool IsGreater<T>(this T first, T second)
            where T : IComparable<T>
        {
            return first.CompareTo(second) > 0;
        }

        public static bool IsLess<T>(this T first, T second)
            where T : IComparable<T>
        {
            return first.CompareTo(second) < 0;
        }

        public static bool IsGreaterOrEqual<T>(this T first, T second)
            where T : IComparable<T>
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool IsLessOrEqual<T>(this T first, T second)
            where T : IComparable<T>
        {
            return first.CompareTo(second) <= 0;
        }

        public static bool IsBetween<T>(this T value, T upper, T lower)
            where T : IComparable<T>
        {
            return value.IsLessOrEqual(upper) 
                && value.IsGreaterOrEqual(lower);
        }

        public static bool IsEqual<T>(this T first, T second)
            where T : IComparable<T>
        {
            return first.CompareTo(second) == 0;
        }
    }
}
