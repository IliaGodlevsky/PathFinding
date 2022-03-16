using Commands.Interfaces;
using System;
using ValueRange.Commands.Realizations;
using ValueRange.Enums;

namespace ValueRange.Commands.Interface
{
    internal interface IReturnByOptionsCommand<T> : IExecutable<ValueWrap<T>>, IExecutionCheck<ReturnOptions>
        where T : IComparable
    {
    }
}
