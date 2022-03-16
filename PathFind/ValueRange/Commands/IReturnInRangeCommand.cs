using Commands.Interfaces;
using System;

namespace ValueRange.Commands
{
    internal interface IReturnInRangeCommand<T> : IExecutable<ValueWrap<T>>, IExecutionCheck<T>
        where T : IComparable
    {
    }
}
