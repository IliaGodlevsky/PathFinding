﻿using Shared.Extensions;
using Shared.Primitives.ValueRange;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shared.Primitives.Extensions
{
    public abstract class ReturnOptions
    {
        public static readonly ReturnOptions Limit = new LimitReturnOptions();
        public static readonly ReturnOptions Cycle = new CycleReturnOptions();

        public static readonly ReadOnlyCollection<ReturnOptions> Options;

        static ReturnOptions()
        {
            Options = new List<ReturnOptions>()
            {
                ReturnOptions.Limit,
                ReturnOptions.Cycle
            }.AsReadOnly();
        }

        internal T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            where T : IComparable<T>
        {
            if (value.IsGreaterThan(range.UpperValueOfRange))
            {
                return GetIfGreater(range);
            }
            else if (value.IsLessThan(range.LowerValueOfRange))
            {
                return GetIfLess(range);
            }
            return value;
        }

        protected abstract T GetIfGreater<T>(InclusiveValueRange<T> range)
            where T : IComparable<T>;

        protected abstract T GetIfLess<T>(InclusiveValueRange<T> range)
            where T : IComparable<T>;

        private sealed class CycleReturnOptions : ReturnOptions
        {
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;

            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;

            public override string ToString() => "Cycle";
        }

        private sealed class LimitReturnOptions : ReturnOptions
        {
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;

            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;

            public override string ToString() => "Limit";
        }
    }
}
