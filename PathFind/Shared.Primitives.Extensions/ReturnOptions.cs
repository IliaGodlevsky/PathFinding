using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;

namespace Shared.Primitives.Extensions
{
    public abstract class ReturnOptions
    {
        public static readonly ReturnOptions Limit = new LimitReturnOptions();
        public static readonly ReturnOptions Cycle = new CycleReturnOptions();

        internal abstract T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            where T : IComparable<T>;

        private sealed class CycleReturnOptions : ReturnOptions
        {
            internal override T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            {
                if (value.IsGreater(range.UpperValueOfRange))
                {
                    return range.LowerValueOfRange;
                }
                else if (value.IsLess(range.LowerValueOfRange))
                {
                    return range.UpperValueOfRange;
                }
                return value;
            }
        }

        private sealed class LimitReturnOptions : ReturnOptions
        {
            internal override T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            {
                if (value.IsGreater(range.UpperValueOfRange))
                {
                    return range.UpperValueOfRange;
                }
                else if (value.IsLess(range.LowerValueOfRange))
                {
                    return range.LowerValueOfRange;
                }
                return value;
            }
        }
    }
}
