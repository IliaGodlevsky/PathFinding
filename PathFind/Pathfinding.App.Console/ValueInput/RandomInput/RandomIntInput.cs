using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;

namespace Pathfinding.App.Console.ValueInput.RandomInput
{
    internal sealed class RandomIntInput : RandomInput<int, int>
    {
        protected override InclusiveValueRange<int> Range { get; }

        public RandomIntInput(IRandom random) : base(random)
        {
            Range = new InclusiveValueRange<int>(0, 10);
        }

        protected override int GetRandomValue()
        {
            return Random.NextInt(Range);
        }
    }
}
