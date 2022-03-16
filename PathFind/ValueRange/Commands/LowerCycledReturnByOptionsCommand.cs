﻿using System;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal sealed class LowerCycledReturnByOptionsCommand<T> : BaseReturnByOptionsCommand<T>
        where T : IComparable
    {
        public LowerCycledReturnByOptionsCommand(InclusiveValueRange<T> range) : base(range)
        {
        }

        public override bool CanExecute(ReturnOptions obj)
        {
            return obj == ReturnOptions.Cycle;
        }

        public override void Execute(ValueWrap<T> obj)
        {
            obj.Value = range.LowerValueOfRange;
        }
    }
}
