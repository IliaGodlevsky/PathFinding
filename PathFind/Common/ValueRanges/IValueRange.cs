using System;

namespace Common.ValueRanges
{
    public interface IValueRange<T>
        where T : IComparable, IComparable<T>
    {
        T UpperValueOfRange { get; }
        T LowerValueOfRange { get; }

        T ReturnInRange(T value);

        bool Contains(T value);
    }
}
