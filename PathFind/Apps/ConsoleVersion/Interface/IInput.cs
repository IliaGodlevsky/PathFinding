using System;

namespace ConsoleVersion.Interface
{
    internal interface IInput<T> where T : IComparable
    {
        T Input();
    }
}
