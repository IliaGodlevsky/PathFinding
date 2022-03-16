using System;
using ValueRange.Commands.Abstractions;
using ValueRange.Enums;

namespace ValueRange.Commands.Realizations
{
    internal sealed class LowerCycledReturnByOptionsCommand<T> : BaseReturnByOptionsCommand<T>
        where T : IComparable
    {
        public LowerCycledReturnByOptionsCommand(InclusiveValueRange<T> range) : base(range)
        {
        }

        public override bool CanExecute(ReturnOptions options)
        {
            return options == ReturnOptions.Cycle;
        }

        public override void Execute(ValueWrap<T> value)
        {
            value.Value = range.LowerValueOfRange;
        }
    }
}
