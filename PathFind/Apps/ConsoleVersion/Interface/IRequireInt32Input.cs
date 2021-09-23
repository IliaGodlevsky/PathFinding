using System;

namespace ConsoleVersion.Interface
{
    internal interface IRequireInt32Input
    {
        IValueInput<int> Int32Input { get; set; }
    }
}
