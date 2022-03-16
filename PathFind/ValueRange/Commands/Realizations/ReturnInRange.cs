using Commands.Extensions;
using System;
using System.Collections.Generic;
using ValueRange.Commands.Interface;
using ValueRange.Enums;

namespace ValueRange.Commands.Realizations
{
    internal sealed class ReturnInRange<T> where T : IComparable
    {
        private IReadOnlyCollection<IReturnInRangeCommand<T>> Commands { get; }

        public ReturnInRange(InclusiveValueRange<T> range, ReturnOptions options)
        {
            Commands = new IReturnInRangeCommand<T>[]
            {
                new LessReturnInRangeCommand<T>(range, options),
                new GreaterReturnInRangeCommand<T>(range, options)
            };
        }

        public T Return(T value)
        {
            var wrap = new ValueWrap<T>(value);
            Commands.ExecuteFirst(value, wrap);
            return wrap.Value;
        }
    }
}
