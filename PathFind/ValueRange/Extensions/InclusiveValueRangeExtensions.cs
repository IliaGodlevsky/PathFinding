using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ValueRange.Enums;

namespace ValueRange.Extensions
{
    public static class InclusiveValueRangeExtensions
    {
        private struct RangeEnumerator
        {
            private readonly int lowerValueOfRange;
            private readonly int upperValueOfRange;

            public int Current { get; private set; }

            internal RangeEnumerator(int lowerValueOfRange, int upperValueOfRange)
            {
                this.lowerValueOfRange = lowerValueOfRange - 1;
                Current = this.lowerValueOfRange;
                this.upperValueOfRange = upperValueOfRange;
            }

            public void Dispose() => Reset();

            public bool MoveNext() => ++Current <= upperValueOfRange;

            public void Reset() => Current = lowerValueOfRange;
        }

        public static ReadOnlyCollection<int> ToReadOnly(this InclusiveValueRange<int> range)
        {
            return range.GetAllValues().ToReadOnly();
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

        private static T ReturnInRangeIfGreaterThanRange<T>(this InclusiveValueRange<T> self, T value, ReturnOptions returnOptions)
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

        private static T ReturnInRangeIfLessThanRange<T>(this InclusiveValueRange<T> self, T value, ReturnOptions returnOptions)
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

        private static IEnumerable<int> GetAllValues(this InclusiveValueRange<int> range)
        {
            foreach (int value in range)
            {
                yield return value;
            }
        }

        private static RangeEnumerator GetEnumerator(this InclusiveValueRange<int> range)
        {
            return new RangeEnumerator(range.LowerValueOfRange, range.UpperValueOfRange);
        }
    }
}
