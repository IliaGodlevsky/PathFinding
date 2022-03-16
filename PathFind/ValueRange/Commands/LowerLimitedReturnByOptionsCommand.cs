using System;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal sealed class LowerLimitedReturnByOptionsCommand<T> : BaseReturnByOptionsCommand<T>
        where T : IComparable
    {
        public LowerLimitedReturnByOptionsCommand(InclusiveValueRange<T> range) : base(range)
        {
        }

        public override bool CanExecute(ReturnOptions obj)
        {
            return obj == ReturnOptions.Limit;
        }

        public override void Execute(ValueWrap<T> obj)
        {
            obj.Value = range.LowerValueOfRange;
        }
    }
}
