﻿using Commands.Extensions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using ValueRange.Enums;

namespace ValueRange.Commands
{
    internal sealed class LessReturnInRangeCommand<T> : BaseReturnInRangeCommand<T>
        where T : IComparable
    {
        protected override IReadOnlyCollection<IReturnByOptionsCommand<T>> Commands { get; }

        public LessReturnInRangeCommand(InclusiveValueRange<T> range, ReturnOptions options) : base(range, options)
        {
            Commands = new IReturnByOptionsCommand<T>[]
            {
                new UpperCycledReturnByOptionsCommand<T>(range),
                new LowerLimitedReturnByOptionsCommand<T>(range)
            };
        }

        public override bool CanExecute(T value)
        {
            return value.IsLess(range.LowerValueOfRange);
        }

        public override void Execute(ValueWrap<T> value)
        {
            Commands.ExecuteFirst(options, value);
        }
    }
}
