using System;

namespace Common.ValueRanges
{
    /// <summary>
    /// Represents range of values (inclusively)
    /// </summary>
    [Serializable]
    public sealed class InclusiveValueRange : BaseValueRange, IValueRange
    {
        /// <summary>
        /// Creates a new instance of <see cref="InclusiveValueRange"/> 
        /// with <paramref name="upperValueOfRange"/> and 
        /// <paramref name="lowerValueOfRange"/> as extreme values of the range
        /// </summary>
        /// <param name="upperValueOfRange"></param>
        /// <param name="lowerValueOfRange"></param>
        /// <remarks>If <paramref name="upperValueOfRange"/> 
        /// is less than <paramref name="lowerValueOfRange"/>
        /// then this parameters will be swapped</remarks>
        public InclusiveValueRange(int upperValueOfRange, int lowerValueOfRange)
            : base(upperValueOfRange, lowerValueOfRange)
        {

        }
    }
}
