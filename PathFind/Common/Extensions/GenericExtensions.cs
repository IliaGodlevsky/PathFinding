using System;

namespace Common.Extensions
{
    public static class GenericExtensions
    {
        public static bool IsGreater<T>(this T first, T second) 
            where T : IComparable, IComparable<T>
        {
            return first.CompareTo(second) > 0;
        }

        public static bool IsLess<T>(this T first, T second) 
            where T : IComparable, IComparable<T>
        {
            return first.CompareTo(second) < 0;
        }

        public static bool IsGreaterOrEqual<T>(this T first, T second) 
            where T : IComparable, IComparable<T>
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool IsLessOrEqual<T>(this T first, T second) 
            where T : IComparable, IComparable<T>
        {
            return first.CompareTo(second) <= 0;
        }

        public static bool IsEqualTo<T>(this T first, T second) 
            where T : IComparable, IComparable<T>
        {
            return first.CompareTo(second) == 0;
        }

        public static bool IsBetween<T>(this T value, T upper, T lower)
            where T : IComparable, IComparable<T>
        {
            return value.IsLessOrEqual(upper) && value.IsGreaterOrEqual(lower);
        }
    }
}
