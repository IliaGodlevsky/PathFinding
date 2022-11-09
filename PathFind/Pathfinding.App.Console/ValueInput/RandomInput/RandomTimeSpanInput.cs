using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;
using System;

namespace Pathfinding.App.Console.ValueInput.RandomInput
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
            return Random.NextTimeSpan(Range);
        }
    }
}