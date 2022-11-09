using Pathfinding.App.Console.Model;
using Shared.Primitives.ValueRange;
using Shared.Random;
using Shared.Random.Extensions;

namespace Pathfinding.App.Console.ValueInput.RandomInput
{
    internal sealed class RandomAnswerInput : RandomInput<Answer, int>
    {
        protected override InclusiveValueRange<int> Range { get; }

        public RandomAnswerInput(IRandom random) : base(random)
        {
            Range = new InclusiveValueRange<int>(Answer.Yes, Answer.No);
        }

        protected override Answer GetRandomValue()
        {
            return (Answer)Random.NextInt(Range);
        }
    }
}