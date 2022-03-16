using Commands.Extensions;
using Common.Extensions;
using System;
using System.Collections.Generic;
using ValueRange.Commands.Abstractions;
using ValueRange.Commands.Interface;
using ValueRange.Enums;

namespace ValueRange.Commands.Realizations
{
    internal sealed class GreaterReturnInRangeCommand<T> : BaseReturnInRangeCommand<T>
        where T : IComparable
    {
        protected override IReadOnlyCollection<IReturnByOptionsCommand<T>> Commands { get; }

        public GreaterReturnInRangeCommand(InclusiveValueRange<T> range, ReturnOptions options) : base(range, options)
        {
            Commands = new IReturnByOptionsCommand<T>[]
            {
                new LowerCycledReturnByOptionsCommand<T>(range),
                new UpperLimitedReturnByOptionsCommand<T>(range)
            };
        }

        public override bool CanExecute(T value)
        {
            return value.IsGreater(range.UpperValueOfRange);
        }

        public override void Execute(ValueWrap<T> value)
        {
            Commands.ExecuteFirst(options, value);
        }
    }
}
