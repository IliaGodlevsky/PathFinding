using System;
using ValueRange.Commands.Abstractions;
using ValueRange.Enums;

namespace ValueRange.Commands.Realizations
{
    internal sealed class UpperLimitedReturnByOptionsCommand<T> : BaseReturnByOptionsCommand<T>
        where T : IComparable
    {
        public UpperLimitedReturnByOptionsCommand(InclusiveValueRange<T> range) : base(range)
        {
        }

        public override bool CanExecute(ReturnOptions options)
        {
            return options == ReturnOptions.Limit;
        }

        public override void Execute(ValueWrap<T> value)
        {
            value.Value = range.UpperValueOfRange;
        }
    }
}
