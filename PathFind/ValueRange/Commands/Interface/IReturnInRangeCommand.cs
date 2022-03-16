using Commands.Interfaces;
using System;
using ValueRange.Commands.Realizations;

namespace ValueRange.Commands.Interface
{
    internal interface IReturnInRangeCommand<T> : IExecutable<ValueWrap<T>>, IExecutionCheck<T>
        where T : IComparable
    {
    }
}
