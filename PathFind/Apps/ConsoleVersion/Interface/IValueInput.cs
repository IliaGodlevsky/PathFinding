using System;

namespace ConsoleVersion.Interface
{
    internal interface IValueInput<TValue>
        where TValue : struct, IComparable
    {
        TValue InputValue(string accompanyingMessage,
            TValue upperRangeValue,
            TValue lowerRangeValue = default);
    }
}
