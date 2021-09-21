using Common.Extensions;
using EnumerationValues.Interface;
using System;
using System.Linq;

namespace EnumerationValues.Extensions
{
    public static class EnumValuesExtensions
    {
        public static Tuple<string, TEnum>[] ToAdjustedAndOrderedByDescriptionTuples<TEnum>(
            this IEnumValues<TEnum> self)
            where TEnum : Enum
        {
            var tuples = self.GetDescriptionAndValueTuples();
            var longestDescriptionLength = tuples.Max(tuple => tuple.Item1.Length);

            return tuples
                .Select(tuple => tuple.Item1.PadRight(longestDescriptionLength))
                .Zip(self.Values, (item1, item2) => new Tuple<string, TEnum>(item1, item2))
                .OrderBy(item => item.Item1)
                .ToArray();
        }

        public static Tuple<string, TEnum>[] GetDescriptionAndValueTuples<TEnum>(this IEnumValues<TEnum> self)
            where TEnum : Enum
        {
            return self.ToTupleCollection(value => value.GetDescriptionAttributeValueOrTypeName());
        }

        public static Tuple<T1, TEnum>[] ToTupleCollection<T1, TEnum>(this IEnumValues<TEnum> self, Func<TEnum, T1> item1Selector)
            where TEnum : Enum
        {
            return self.ToTupleCollection(item1Selector, value => value);
        }

        public static Tuple<T1, T2>[] ToTupleCollection<T1, T2, TEnum>(this IEnumValues<TEnum> self, Func<TEnum, T1> item1Selector,
            Func<TEnum, T2> item2Selector)
            where TEnum : Enum
        {
            return self.Values
                .Select(value => new Tuple<T1, T2>(item1Selector(value), item2Selector(value)))
                .ToArray();
        }
    }
}
