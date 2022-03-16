using System;
using ValueRange.Commands.Interface;
using ValueRange.Commands.Realizations;
using ValueRange.Enums;

namespace ValueRange.Commands.Abstractions
{
    internal abstract class BaseReturnByOptionsCommand<T> : IReturnByOptionsCommand<T>
        where T : IComparable
    {
        protected BaseReturnByOptionsCommand(InclusiveValueRange<T> range)
        {
            this.range = range;
        }

        public abstract bool CanExecute(ReturnOptions option);

        public abstract void Execute(ValueWrap<T> value);

        protected readonly InclusiveValueRange<T> range;
    }
}
