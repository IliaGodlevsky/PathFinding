using Common.Extensions;
using Random.Extensions;
using Random.Interface;
using System;
using System.Diagnostics;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomKeyInput : RandomInput<ConsoleKey>
    {
        protected override int WaitMilliseconds => 500;

        private ConsoleKey[] AvaliableKeys { get; }

        protected override InclusiveValueRange<int> Range { get; }

        public RandomKeyInput(IRandom random) : base(random)
        {
            AvaliableKeys = new[] 
            { 
                ConsoleKey.Enter, 
                ConsoleKey.UpArrow, 
                ConsoleKey.DownArrow, 
                ConsoleKey.P 
            };

            Range = new InclusiveValueRange<int>(AvaliableKeys.Length - 1);
        }

        public override ConsoleKey Input()
        {
            int value = GetRandomInt();
            ConsoleKey key = ConvertFrom(value);
            Stopwatch.StartNew().Wait(WaitMilliseconds);
            return key;
        }

        protected override ConsoleKey ConvertFrom(int value)
        {
            return AvaliableKeys[value];
        }

        protected override int GetRandomInt()
        {
            return Random.Next(Range);
        }
    }
}
