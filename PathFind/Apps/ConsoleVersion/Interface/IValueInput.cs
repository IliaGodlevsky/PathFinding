using System;

namespace ConsoleVersion.Interface
{
    internal interface IValueInput<T> where T : struct, IComparable
    {
        T InputValue(string msg, T upper, T lower = default);
    }
}
