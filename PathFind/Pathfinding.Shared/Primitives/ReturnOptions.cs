using Pathfinding.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Pathfinding.Shared.Primitives
{
    public abstract class ReturnOptions
    {
        public static readonly ReturnOptions Limit = new LimitReturnOptions();
        public static readonly ReturnOptions Cycle = new CycleReturnOptions();

        public static readonly IReadOnlyCollection<ReturnOptions> Options;

        static ReturnOptions()
        {
            Options = new List<ReturnOptions>() { Limit, Cycle }.AsReadOnly();
        }

        internal T ReturnInRange<T>(T value, InclusiveValueRange<T> range)
            where T : IComparable<T>
        {
            if (value.IsGreaterThan(range.UpperValueOfRange)) return GetIfGreater(range);
            else if (value.IsLessThan(range.LowerValueOfRange)) return GetIfLess(range);
            return value;
        }

        public abstract override string ToString();

        protected abstract T GetIfGreater<T>(InclusiveValueRange<T> range) where T : IComparable<T>;

        protected abstract T GetIfLess<T>(InclusiveValueRange<T> range) where T : IComparable<T>;

        private sealed class CycleReturnOptions : ReturnOptions
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString() => "Cycle";
        }

        private sealed class LimitReturnOptions : ReturnOptions
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override T GetIfGreater<T>(InclusiveValueRange<T> range) => range.UpperValueOfRange;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            protected override T GetIfLess<T>(InclusiveValueRange<T> range) => range.LowerValueOfRange;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override string ToString() => "Limit";
        }
    }
}
