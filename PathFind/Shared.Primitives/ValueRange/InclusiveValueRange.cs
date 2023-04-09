using Shared.Extensions;
using System;
using System.Diagnostics;

namespace Shared.Primitives.ValueRange
{
    /// <summary>
    /// Representa an inclusive value range, value of which should 
    /// implement <see cref="IComparable{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of values, that 
    /// will represent the range</typeparam>
    [DebuggerDisplay("[{LowerValueOfRange}...{UpperValueOfRange}]")]
    public readonly record struct InclusiveValueRange<T>
        where T : IComparable<T>
    {
        public readonly T UpperValueOfRange { get; }

        public readonly T LowerValueOfRange { get; }

        /// <param name="upperValueOfRange"></param>
        /// <param name="lowerValueOfRange"></param>
        /// <remarks>No matter which value is greater, 
        /// value are assigned correctly anyway</remarks>
        public InclusiveValueRange(T upperValueOfRange, T lowerValueOfRange = default)
        {
            if (upperValueOfRange.IsLess(lowerValueOfRange))
            {
                UpperValueOfRange = lowerValueOfRange;
                LowerValueOfRange = upperValueOfRange;
            }
            else
            {
                UpperValueOfRange = upperValueOfRange;
                LowerValueOfRange = lowerValueOfRange;
            }
        }
    }
}
