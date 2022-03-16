using System;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal abstract class BaseReturnByOptionsCommand<T> : IReturnByOptionsCommand<T>
        where T : IComparable
    {
        protected BaseReturnByOptionsCommand(InclusiveValueRange<T> range)
        {
            this.range = range;
        }

        public abstract bool CanExecute(ReturnOptions obj);

        public abstract void Execute(ValueWrap<T> obj);

        protected readonly InclusiveValueRange<T> range;
    }
}
