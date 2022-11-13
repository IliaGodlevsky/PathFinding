﻿using Pathfinding.App.Console.Interface;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using System;

using ColorfulConsole = Colorful.Console;

namespace Pathfinding.App.Console.ValueInput.RandomInput
{
    internal abstract class RandomInput<T, TRange> : IInput<T>
        where TRange : IComparable, IComparable<TRange>
    {
        protected virtual TimeSpan Delay { get; }

        protected IRandom Random { get; }

        protected abstract InclusiveValueRange<TRange> Range { get; }

        protected RandomInput(IRandom random)
        {
            Delay = TimeSpan.FromMilliseconds(500);
            Random = random;
        }

        public virtual T Input()
        {
            T value = GetRandomValue();
            ColorfulConsole.WriteLine(value);
            Delay.Wait();
            return value;
        }

        protected abstract T GetRandomValue();
    }
}