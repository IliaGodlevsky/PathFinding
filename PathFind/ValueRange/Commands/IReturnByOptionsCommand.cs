using Commands.Interfaces;
using System;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal interface IReturnByOptionsCommand<T> : IExecutable<ValueWrap<T>>, IExecutionCheck<ReturnOptions>
        where T : IComparable
    {
    }
}
