using System;

namespace Common.ValueRanges
{
    [Serializable]
    public sealed class UpInclusiveValueRange : BaseValueRange, IValueRange
    {
        public UpInclusiveValueRange(int upperValueOfRange, int lowerValueOfRange)
            : base(upperValueOfRange, lowerValueOfRange + 1)
        {
            if (upperValueOfRange == lowerValueOfRange)
            {
                throw new ArgumentException();
            }
        }
    }
}
