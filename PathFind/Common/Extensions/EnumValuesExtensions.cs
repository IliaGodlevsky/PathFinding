using System;
using System.Linq;

namespace Common.Extensions
{
    public static class EnumValuesExtensions
    {
        public static Tuple<string, TEnum>[] ToAdjustedAndOrderedByDescriptionTuples<TEnum>(
            this EnumValues<TEnum> self)
            where TEnum : Enum
        {
            var tuples = self.GetDescriptionAndValueTuples();
            var longestDescriptionLength = tuples.Max(tuple => tuple.Item1.Length);
            string PadRight(string description) => description.PadRight(longestDescriptionLength);
            Tuple<string, TEnum> ToTuple(string item1, TEnum item2) 
                => new Tuple<string, TEnum>(item1, item2);

            return tuples
                .Select(tuple => PadRight(tuple.Item1))
                .Zip(self.Values, ToTuple)
                .OrderBy(item => item.Item1)
                .ToArray();
        }

        public static Tuple<string, TEnum>[] GetDescriptionAndValueTuples<TEnum>(this EnumValues<TEnum> self)
            where TEnum : Enum
        {
            return self.ToTupleCollection(value => value.GetDescriptionAttributeValueOrTypeName());
        }

        public static Tuple<T1, TEnum>[] ToTupleCollection<T1, TEnum>(this EnumValues<TEnum> self, Func<TEnum, T1> item1Selector)
            where TEnum : Enum
        {
            return self.ToTupleCollection(item1Selector, value => value);
        }

        public static Tuple<T1, T2>[] ToTupleCollection<T1, T2, TEnum>(this EnumValues<TEnum> self, Func<TEnum, T1> item1Selector,
            Func<TEnum, T2> item2Selector)
            where TEnum : Enum
        {
            return self.Values
                .Select(value => new Tuple<T1, T2>(item1Selector(value), item2Selector(value)))
                .ToArray();
        }
    }
}
