using Shared.Extensions;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;
using System;
using System.Collections.Generic;
using static System.ConsoleKey;

namespace Pathfinding.App.Console.ValueInput.RandomInput
{
    internal sealed class RandomKeyInput : RandomInput<ConsoleKey, int>
    {
        private IReadOnlyList<ConsoleKey> Keys { get; }

        protected override InclusiveValueRange<int> Range { get; }

        public RandomKeyInput(IRandom random) : base(random)
        {
            Keys = new List<ConsoleKey>() { Enter/*, UpArrow, DownArrow*/};
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
