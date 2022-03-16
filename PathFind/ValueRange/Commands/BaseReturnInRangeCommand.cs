using System;
using System.Collections.Generic;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal abstract class BaseReturnInRangeCommand<T> : IReturnInRangeCommand<T>
        where T : IComparable
    {
        protected abstract IReadOnlyCollection<IReturnByOptionsCommand<T>> Commands { get; }

        protected BaseReturnInRangeCommand(InclusiveValueRange<T> range, ReturnOptions options)
        {
            this.range = range;
            this.options = options;
        }

        public abstract bool CanExecute(T obj);

        public abstract void Execute(ValueWrap<T> obj);

        protected readonly InclusiveValueRange<T> range;
        protected readonly ReturnOptions options;
    }
}
