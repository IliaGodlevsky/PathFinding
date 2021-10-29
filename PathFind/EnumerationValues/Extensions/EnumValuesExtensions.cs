using Common.ValueRanges;
using EnumerationValues.Interface;
using System;
using System.Linq;

namespace EnumerationValues.Extensions
{
    public static class EnumValuesExtensions
    {
        public static Tuple<T1, TEnum>[] ToTupleCollection<T1, TEnum>(this IEnumValues<TEnum> self, Func<TEnum, T1> item1Selector)
            where TEnum : Enum
        {
            return self.ToTupleCollection(item1Selector, value => value);
        }

        public static InclusiveValueRange<TEnum> ToValueRange<TEnum>(this IEnumValues<TEnum> self)
            where TEnum : struct, Enum
        {
            var maxValue = self.Values.Max();
            var minValue = self.Values.Min();
            return new InclusiveValueRange<TEnum>(maxValue, minValue);
        }

        public static Tuple<T1, T2>[] ToTupleCollection<T1, T2, TEnum>(this IEnumValues<TEnum> self, Func<TEnum, T1> item1Selector,
            Func<TEnum, T2> item2Selector)
            where TEnum : Enum
        {
            return self.Values.Select(value => new Tuple<T1, T2>(item1Selector(value), item2Selector(value))).ToArray();
        }
    }
}
