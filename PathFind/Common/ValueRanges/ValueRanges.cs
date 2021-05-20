using System.Linq;

namespace Common.ValueRanges
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Sensitive to the order of elements</remarks>
    public sealed class ValueRanges
    {
        public ValueRanges(params IValueRange[] ranges)
        {
            this.ranges = ranges;
            rangesCount = ranges.Length;
        }

        public bool Contains(params int[] values)
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

        private readonly IValueRange[] ranges;
        private readonly int rangesCount;
    }
}
