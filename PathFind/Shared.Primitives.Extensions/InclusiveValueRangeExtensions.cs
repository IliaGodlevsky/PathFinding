using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;

namespace Shared.Primitives.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        /// <summary>
        /// Returns all values from <paramref name="range"/>
        /// </summary>
        /// <param name="range"></param>
        /// <returns>An enumerable, that contains all 
        /// values from <paramref name="range"/></returns>
        public static IEnumerable<int> Enumerate(this InclusiveValueRange<int> range)
        {
            for (int value = range.LowerValueOfRange; value <= range.UpperValueOfRange; value++)
            {
                yield return value;
            }
        }

        public static long Amplitude(this InclusiveValueRange<int> valueRange)
        {
            return (long)valueRange.UpperValueOfRange - valueRange.LowerValueOfRange;
        }

        public static double Amplitude(this InclusiveValueRange<double> valueRange)
        {
            return valueRange.UpperValueOfRange - valueRange.LowerValueOfRange;
        }

        public static bool Contains<T>(this InclusiveValueRange<T> self, T value)
            where T : IComparable<T>
        {
            return value.IsBetween(self.UpperValueOfRange, self.LowerValueOfRange);
        }

        public static T ReturnInRange<T>(this InclusiveValueRange<T> self, T value, ReturnOptions options)
            where T : IComparable<T>
        {
            return options.ReturnInRange(value, self);
        }

        public static T ReturnInRange<T>(this InclusiveValueRange<T> self, T value)
            where T : IComparable<T>
        {
            return self.ReturnInRange(value, ReturnOptions.Limit);
        }
    }
}
