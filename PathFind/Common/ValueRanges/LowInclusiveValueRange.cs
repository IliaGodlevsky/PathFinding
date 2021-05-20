using System;

namespace Common.ValueRanges
{
    [Serializable]
    /// <summary>
    /// Represents range of values (inclusively only lower value of range)
    /// </summary>
    public sealed class LowInclusiveValueRange : BaseValueRange, IValueRange
    {
        /// <summary>
        /// Creates a new instance of <see cref="LowInclusiveValueRange"/> 
        /// with <paramref name="upperValueOfRange"/> and 
        /// <paramref name="lowerValueOfRange"/> as extreme values of the range
        /// </summary>
        /// <param name="upperValueOfRange"></param>
        /// <param name="lowerValueOfRange"></param>
        /// <remarks>If <paramref name="upperValueOfRange"/> 
        /// is less than <paramref name="lowerValueOfRange"/>
        /// then this parameters will be swapped;
        /// <paramref name="lowerValueOfRange"/> is inclusive value
        /// <paramref name="upperValueOfRange"/> is not inclusive value</remarks>
        public LowInclusiveValueRange(int upperValueOfRange, int lowerValueOfRange)
            : base(upperValueOfRange - 1, lowerValueOfRange)
        {
            if (upperValueOfRange == lowerValueOfRange)
            {
                string message = $"An error ocurred while creating {nameof(LowInclusiveValueRange)}";
                message += $"{nameof(upperValueOfRange)} is equal to {nameof(lowerValueOfRange)}";
                throw new ArgumentException(message);
            }
        }
    }
}
