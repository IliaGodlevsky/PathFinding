using ConsoleVersion.ValueInput.Interface;
using System;

namespace ConsoleVersion.ViewModel
{
    internal interface IRequireInt32Input
    {
        IValueInput<int> Int32Input { get; set; }
    }
}
