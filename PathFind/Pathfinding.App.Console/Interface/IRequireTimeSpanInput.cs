using System;

namespace Pathfinding.App.Console.Interface
{
    internal interface IRequireTimeSpanInput
    {
        IInput<TimeSpan> TimeSpanInput { get; set; }
    }
}
