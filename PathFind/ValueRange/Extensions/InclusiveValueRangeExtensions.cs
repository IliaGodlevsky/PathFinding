using Common.Extensions;
using System;
using System.Collections.Generic;
using ValueRange.Enums;

namespace ValueRange.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        public static IEnumerable<int> GetAllValuesInRange(this InclusiveValueRange<int> valueRange)
        {
            for (int i = valueRange.LowerValueOfRange; i <= valueRange.UpperValueOfRange; i++)
            {
                yield return i;
            }
        }

        public static uint Amplitude(this InclusiveValueRange<int> valueRange)
        {
            return (uint)((long)valueRange.UpperValueOfRange - valueRange.LowerValueOfRange);
        }

        public static double Amplitude(this InclusiveValueRange<double> valueRange)
        {
            return valueRange.UpperValueOfRange - valueRange.LowerValueOfRange;
        }

        public static bool Contains<T>(this InclusiveValueRange<T> self, T value)
            where T : struct, IComparable
        {
            return value.IsBetween(self.UpperValueOfRange, self.LowerValueOfRange);
        }

        public static T ReturnInRange<T>(this InclusiveValueRange<T> self, T value,
            ReturnOptions returnOptions = ReturnOptions.Limit)
            where T : struct, IComparable
        {
            if (value.IsGreater(self.UpperValueOfRange))
            {
                switch (returnOptions)
                {
                    case ReturnOptions.Cycle: value = self.LowerValueOfRange; break;
                    case ReturnOptions.Limit: value = self.UpperValueOfRange; break;
                }
            }
            else if (value.IsLess(self.LowerValueOfRange))
            {
                switch (returnOptions)
                {
                    case ReturnOptions.Cycle: value = self.UpperValueOfRange; break;
                    case ReturnOptions.Limit: value = self.LowerValueOfRange; break;
                }
            }
            return value;
        }
    }
}
