using Common.Extensions;
using Random.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomKeyInput : RandomInput<ConsoleKey>
    {
        private ConsoleKey[] AvailableKeys { get; }

        protected override InclusiveValueRange<int> Range { get; }

        public RandomKeyInput(IRandom random) : base(random)
        {
            AvailableKeys = new[] { ConsoleKey.Enter, ConsoleKey.UpArrow, ConsoleKey.DownArrow };
            Range = new InclusiveValueRange<int>(AvailableKeys.Length - 1);
        }

        public override ConsoleKey Input()
        {
            int value = GetRandomInt();
            ConsoleKey key = ConvertFrom(value);
            Delay.Wait();
            return key;
        }

        protected override ConsoleKey ConvertFrom(int value)
        {
            return AvailableKeys[value];
        }
    }
}
