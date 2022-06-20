using System;

namespace ConsoleVersion.Interface
{
    internal interface IRequireTimeSpanInput
    {
        IInput<TimeSpan> TimeSpanInput { get; set; }
    }
}
