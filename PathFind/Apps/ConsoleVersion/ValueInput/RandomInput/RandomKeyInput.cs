using Common.Extensions;
using Random.Extensions;
using Random.Interface;
using System;
using System.Collections.Generic;
using ValueRange;

using static System.ConsoleKey;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomKeyInput : RandomInput<ConsoleKey, int>
    {
        private IReadOnlyList<ConsoleKey> Keys { get; }

        protected override InclusiveValueRange<int> Range { get; }

        public RandomKeyInput(IRandom random) : base(random)
        {
            Keys = new[] { Enter, UpArrow, DownArrow };
            Range = new InclusiveValueRange<int>(Keys.Count - 1);
        }

        public override ConsoleKey Input()
        {
            Delay.Wait();
            return GetRandomValue();
        }

        protected override ConsoleKey GetRandomValue()
        {
            int random = Random.Next(Range);
            return Keys[random];
        }
    }
}
