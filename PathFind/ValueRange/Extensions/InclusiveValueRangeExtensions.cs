using Commands.Extensions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using ValueRange.Commands;
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

        public static InclusiveValueRange<T> ToRange<T>(this (T upper, T lower) range)
            where T : IComparable
        {
            return new InclusiveValueRange<T>(range.upper, range.lower);
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
            where T : IComparable
        {
            return value.IsBetween(self.UpperValueOfRange, self.LowerValueOfRange);
        }

        public static T ReturnInRange<T>(this InclusiveValueRange<T> self, T value,
            ReturnOptions returnOptions = ReturnOptions.Limit)
            where T : IComparable
        {
            var wrap = new ValueWrap<T>(value);
            var commands = new IReturnInRangeCommand<T>[]
            {
                new LessReturnInRangeCommand<T>(self, returnOptions),
                new GreaterReturnInRangeCommand<T>(self, returnOptions)
            };
            commands.ExecuteFirst(value, wrap);
            return wrap.Value;
        }
    }
}
