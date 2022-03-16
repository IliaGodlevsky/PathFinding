using System;
using ValueRange.Commands.Abstractions;
using ValueRange.Enums;

namespace ValueRange.Commands.Realizations
{
    internal sealed class UpperCycledReturnByOptionsCommand<T> : BaseReturnByOptionsCommand<T>
        where T : IComparable
    {
        public UpperCycledReturnByOptionsCommand(InclusiveValueRange<T> range) : base(range)
        {
        }

        public override bool CanExecute(ReturnOptions options)
        {
            return options == ReturnOptions.Cycle;
        }

        public override void Execute(ValueWrap<T> value)
        {
            value.Value = range.UpperValueOfRange;
        }
    }
}
