using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Shared.Primitives.Extensions
{
    public abstract class ReturnOptions
    {
        public static readonly ReturnOptions Limit = new LimitReturnOptions();
        public static readonly ReturnOptions Cycle = new CycleReturnOptions();

        internal T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            where T : IComparable<T>
        {
            if (value.IsGreaterThan(range.UpperValueOfRange))
            {
                return GetIfGreater(range);
            }
            else if (value.IsLessThan(range.LowerValueOfRange))
            {
                return GetIfLess(range);
            }
            return value;
        }

        protected abstract T GetIfGreater<T>(InclusiveValueRange<T> range)
            where T : IComparable<T>;

        protected abstract T GetIfLess<T>(InclusiveValueRange<T> range)
            where T : IComparable<T>;

        private sealed class CycleReturnOptions : ReturnOptions
        {
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;

            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;
        }

        private sealed class LimitReturnOptions : ReturnOptions
        {
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;

            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;
        }
    }
}
