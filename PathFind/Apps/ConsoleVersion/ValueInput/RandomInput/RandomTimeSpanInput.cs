using Random.Extensions;
using Random.Interface;
using System;
using ValueRange;

namespace ConsoleVersion.ValueInput.RandomInput
{
    internal sealed class RandomTimeSpanInput : RandomInput<TimeSpan, TimeSpan>
    {
        protected override InclusiveValueRange<TimeSpan> Range { get; }

        public RandomTimeSpanInput(IRandom random) : base(random)
        {
            Range = Constants.AlgorithmDelayTimeValueRange;
        }

        protected override TimeSpan GetRandomValue()
        {
            return Random.Next(Range);
        }
    }
}