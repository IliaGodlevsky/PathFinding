using Common.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace ValueRange
{
    [DataContract]
    [Serializable]
    [DebuggerDisplay("[{LowerValueOfRange} - {UpperValueOfRange}]")]
    public readonly struct InclusiveValueRange<T> where T : IComparable
    {
        [DataMember]
        public T UpperValueOfRange { get; }

        [DataMember]
        public T LowerValueOfRange { get; }

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
