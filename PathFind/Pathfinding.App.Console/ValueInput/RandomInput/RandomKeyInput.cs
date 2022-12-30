﻿using Shared.Collections;
using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;
using System;

using static System.ConsoleKey;

namespace Pathfinding.App.Console.ValueInput.RandomInput
{
    internal sealed class RandomKeyInput : RandomInput<ConsoleKey, int>
    {
        private ReadOnlyList<ConsoleKey> Keys { get; }

        protected override InclusiveValueRange<int> Range { get; }

        public RandomKeyInput(IRandom random) : base(random)
        {
            Keys = new ReadOnlyList<ConsoleKey>(Enter/*, UpArrow, DownArrow*/);
            Range = new InclusiveValueRange<int>(Keys.Count - 1);
        }

        public override ConsoleKey Input()
        {
            Delay.Wait();
            return GetRandomValue();
        }

        protected override ConsoleKey GetRandomValue()
        {
            int random = Random.NextInt(Range);
            return Keys[random];
        }
    }
}
