using System;
using System.Linq;

namespace Common.ValueRanges
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Sensitive to the order of elements</remarks>
    public sealed class ValueRanges<T>
        where T : IComparable, IComparable<T>
    {
        public ValueRanges(params IValueRange<T>[] ranges)
        {
            this.ranges = ranges;
            rangesCount = ranges.Length;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <remarks>Sensitive to number of incoming values</remarks>
        public bool Contains(params T[] values)
        {
            if (values.Length != ranges.Length)
            {
                return false;
            }

            bool Contains(int index)
            {
                return ranges[index].Contains(values[index]);
            }

            return Enumerable.Range(0, rangesCount).All(Contains);
        }

        private readonly IValueRange<T>[] ranges;
        private readonly int rangesCount;
    }
}
