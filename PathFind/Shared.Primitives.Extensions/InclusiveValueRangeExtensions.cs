using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Primitives.ValueRange.Enums;
using System;
using System.Collections.Generic;

namespace Shared.Primitives.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        public static IEnumerable<int> EnumerateValues(this InclusiveValueRange<int> range)
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
            where T : IComparable, IComparable<T>
        {
            return value.IsBetween(self.UpperValueOfRange, self.LowerValueOfRange);
        }

        public static T ReturnInRange<T>(this InclusiveValueRange<T> self, T value,
            ReturnOptions returnOptions = ReturnOptions.Limit)
            where T : IComparable, IComparable<T>
        {
            if (value.IsGreater(self.UpperValueOfRange))
            {
                return self.ReturnInRangeIfGreaterThanRange(value, returnOptions);
            }
            else if (value.IsLess(self.LowerValueOfRange))
            {
                return self.ReturnInRangeIfLessThanRange(value, returnOptions);
            }
            return value;
        }

        private static T ReturnInRangeIfGreaterThanRange<T>(this InclusiveValueRange<T> self, T value, 
            ReturnOptions returnOptions)
            where T : IComparable, IComparable<T>
        {
            switch (returnOptions)
            {
                case ReturnOptions.Cycle:
                    return self.LowerValueOfRange;
                case ReturnOptions.Limit:
                    return self.UpperValueOfRange;
                default:
                    return value;
            }
        }

        private static T ReturnInRangeIfLessThanRange<T>(this InclusiveValueRange<T> self, T value, 
            ReturnOptions returnOptions)
            where T : IComparable, IComparable<T>
        {
            switch (returnOptions)
            {
                case ReturnOptions.Cycle:
                    return self.UpperValueOfRange;
                case ReturnOptions.Limit:
                    return self.LowerValueOfRange;
                default:
                    return value;
            }
        }
    }
}
