using EnumerationValues.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ValueRange;

namespace EnumerationValues.Extensions
{
    public static class EnumValuesExtensions
    {
        public static InclusiveValueRange<TEnum> ToValueRange<TEnum>(this IEnumValues<TEnum> self)
            where TEnum : struct, Enum
        {
            var maxValue = self.Values.Max();
            var minValue = self.Values.Min();
            return new InclusiveValueRange<TEnum>(maxValue, minValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<T1, T2>[] ToTupleCollection<T1, T2, TEnum>(this IEnumValues<TEnum> self, Func<TEnum, T1> item1Selector,
            Func<TEnum, T2> item2Selector)
            where TEnum : Enum
        {
            return self.Values.Select(value => new Tuple<T1, T2>(item1Selector(value), item2Selector(value))).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TEnum> BreakIntoFlags<TEnum>(this IEnumValues<TEnum> values, TEnum value)
            where TEnum : Enum
        {
            return values.Values.Where(val => value.HasFlag(val));
        }
    }
}
